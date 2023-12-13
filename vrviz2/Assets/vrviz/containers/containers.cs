using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;

namespace VRViz.Containers {

    public class SceneConfig {
        public List<Display> displays { get; set; }
        public Dictionary<string, Display> displays_dictionary = new Dictionary<string, Display>{};

        public void convert_displays_to_dict () {
            Profiler.BeginSample("VRViz.Containers::SceneConfig.convert_displays_to_dict");
            foreach (Display d in this.displays) { this.displays_dictionary[d.container.mqtt_reference] = d; }
            this.displays = null;
            Profiler.EndSample();
        }

        public void handle_incoming_message(object sender, MqttMsgPublishEventArgs raw_msg) {
            //Convert raw message to a string, and that message into a json object matching our type
            string msg = System.Text.Encoding.UTF8.GetString(raw_msg.Message);
            var json = JsonConvert.DeserializeObject(msg, this.displays_dictionary[raw_msg.Topic].container.msg_type_typ);
            this.displays_dictionary[raw_msg.Topic].container.message_data = json;
        }
    }



    public class Display {
        //Config-loaded properties
        public string visualization { get; set; }
        public string reference { get; set; }
        public Dictionary<string, string> topic_details { get; set; }
        public Dictionary<string, string> display_details { get; set; }

        //Generated properties
        public Dictionary<string, object> display_details_obj { get; set; }
        public Base container;

        public void construct_container() {
            Profiler.BeginSample("VRViz.Containers::Display.construct_container");
            //Decode dictionaries into relevant types
            try { this.display_details_obj = Utils.decodeDisplayDictionary(this.display_details); }
            catch { this.display_details_obj = new Dictionary<string, object>(); }

            //Identify datatype of required container
            string aqn = typeof(Display).AssemblyQualifiedName.Replace("VRViz.Containers.Display", "VRViz.Containers."+this.visualization);
            Type container_type = Type.GetType(aqn);

            //Attempt to construct the container
            try { this.container = (Base) Activator.CreateInstance(container_type, new object[]{this.reference, this.display_details_obj, this.topic_details}); }
            catch (Exception e) { Debug.Log(aqn + ": Not found " + e); }
            Profiler.EndSample();
        }
    }


    // Base class for containers defined within `used.cs`
    public abstract class Base {

        //Apply the message contents to the scene
        //public bool locked = false;
        public void ApplyIfMessage() {
            Profiler.BeginSample("VRViz.Containers::Base.ApplyIfMessage");
            if (this.message_data != null) {
                this.ApplyMessage();
            }
            this.message_data = null;
            Profiler.EndSample();
        }

        //DefineFrame(this.frame, data.header.frame_id('camera_depth_optical_frame'))
        //this creates the frame, we are pointing relative to...
        public Transform DefineFrame(Transform link, string name) {
            //GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/vrviz/prefabs/frame_.prefab", typeof(GameObject));
            GameObject prefab = Resources.Load<GameObject>("Assets/vrviz/prefabs/frame_.prefab");
            Debug.LogWarning(link);
            Debug.LogWarning(name);
            Debug.LogWarning(prefab);
            Debug.LogWarning((Transform)link.Find(name));
            if ((Transform)link.Find(name) == null) {
                GameObject child_go = (GameObject)UnityEngine.Object.Instantiate(prefab, link);
                child_go.name = name;
            }

            return link.Find(name);
        }

        public abstract void ApplyMessage();

        //Define the msg type and ref_type as topic or param
        public abstract string msg_type { get; set; }
        public abstract string reference_type { get; set; }
        public object message_data { get; set; }

        //Identify properties to be defined in base class
        public string reference { get; set; }
        public string mqtt_reference { get; set; }
        private string control_topic { get; set; }
        private string frequency { get; set; }
        private string latched { get; set; }
        private string qos { get; set; }
        public Type   msg_type_typ { get; set; }
        public string msg_type_msg { get; set; }
        public string msg_type_pkg { get; set; }
        public bool has_new_message { get; set; }

        //Accept a reference definition from a child class
        public Base(string R, Dictionary<string, string> T){
            //Convert topic reference to applicable rosparam reference and mqtt reference
            //  (/map) > (/vrviz/mqtt)
            //  (/rob_desc > /vrviz/rosparam/rob_desc) > (/vrviz/rosparam/rob_desc)
            this.reference =      this.reference_type == "rosparam" ? "/vrviz/rosparam"+R : R;
            this.mqtt_reference = this.reference_type == "rosparam" ? "vrviz/rosparam"+R : "vrviz"+R;

            //Format remaining mqtt details
            try { this.control_topic = T["control_topic"]; }  catch { this.control_topic = "__dynamic_server"; }
            try { this.frequency = T["frequency"]; }          catch { this.frequency = "1.0"; }
            try { this.latched = T["latched"].ToLower(); }    catch { this.latched = "false"; }
            try { this.qos = T["qos"]; }                      catch { this.qos = "1"; }

            //Define faster refrences for message type labels
            this.msg_type_typ = Type.GetType("VRViz.Messages."+this.msg_type.Replace("/", "."), true);
            this.msg_type_msg = this.msg_type.Replace("/", ".msg:");
            this.msg_type_pkg = this.msg_type;
        }

        //Display details of the class
        public void Describe() {
            string msg = "Container defined as type <{0}> pointing to {1} '{2}'";
            Debug.Log(string.Format(msg, this.msg_type, this.reference_type, this.reference));
        }

        //Open the mqtt connection for this display
        public void OpenTopic (MqttClient client) {
            string ros_topic = this.reference_type == "rostopic" ? this.reference : "/vrviz/rosparam"+this.reference;

            //Format message to open topic
            string open_message = String.Format(@"
            {{""op"": ""{0}"",
             ""args"":{{
                    ""topic_from"":""{1}"",
                    ""topic_to"":""{2}"",
                    ""msg_type"":""{3}"",
                    ""frequency"":{4},
                    ""latched"":{5},
                    ""qos"":{6}
                    }}
            }}",
            "ros2mqtt_subscribe", ros_topic, this.mqtt_reference, this.msg_type_msg, this.frequency, this.latched, this.qos);
            //Debug.Log(open_message);


            //Construct and publish a message to the dynamic bridge server to open the topic of interest
            byte[] mqtt_topic_initiator = System.Text.Encoding.UTF8.GetBytes(open_message);
            string topic_opener = this.control_topic+"/topic/vrviz_ros";
            client.Publish(topic_opener, mqtt_topic_initiator, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

            //Subscribe to the opened topic
            client.Subscribe(new string[] { this.mqtt_reference }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        public void on_message(string msg) {

            // convert string to json object
            var json = JsonConvert.DeserializeObject(msg, this.msg_type_typ);
            Debug.Log("From " + this.reference + ", processing: " + json);

            // save message to associated display
            this.message_data = json;
            this.has_new_message = true;

            //this.ApplyMessage(); //this proves this is in coroutine

        }

    }
}
