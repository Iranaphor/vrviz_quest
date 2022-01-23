using System;
using System.Collections.Generic;
using UnityEngine;

// using uPLibrary.Networking.M2Mqtt;
// using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

using VRViz;
using VRViz.Communications;
using geometry_msgs = VRViz.Messages.geometry_msgs;
using vrviz_msgs = VRViz.Messages.vrviz_ros;

using Newtonsoft.Json;

namespace M2MqttUnity.Examples {
    public class M2MqttUnityNew : M2MqttUnityTest {
        private bool missing_config = true;
        public bool show_model = true;

        private static Dictionary<string, MessageDefinition> message_definitions = new Dictionary<string, MessageDefinition>();
   
        protected override void SubscribeTopics() { new TopicOpener("/vrviz/config", "vrviz_ros/SystemConfig", new vrviz_msgs::MqttParameters(), client); } //I predict this will break
        protected override void Start() { base.Start(); }

        protected override void DecodeMessage(string topic, byte[] message) {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            // Debug.Log(msg);
            
            if (topic == "vrviz/vrviz/config" && missing_config) {
                missing_config = false;
                TopicOpener.unsubscribe(client, "vrviz/vrviz/config");
                
                vrviz_msgs::SystemConfig tl = JsonConvert.DeserializeObject<vrviz_msgs::SystemConfig>(msg);
                // Spawn frame for each frame listed in config
                // if (show_model) {
                    string prefix = "frame___";
                    
                    foreach(geometry_msgs::TransformStamped tf in tl.frame_list){
                        new GameObject(prefix+tf.child_frame_id.data);
                    }
                    foreach(geometry_msgs::TransformStamped tf in tl.frame_list){
                        GameObject parent = GameObject.Find(prefix+tf.header.frame_id.data);
                        GameObject child = GameObject.Find(prefix+tf.child_frame_id.data);
                        child.transform.SetParent(parent.GetComponent<Transform>());
                        Modifiers.SetTransformOfFrame(tf.transform, child);
                    }
                // }
                // Open topic for each message listed in config
                foreach(vrviz_msgs::Topic t in tl.topic_list){
                    new TopicOpener(t.topic.data, t.msg_type.data, t.mqtt, client);
                    message_definitions.Add(topic, new MessageDefinition(t.unity, t.msg_type.data));
                }

            }
            message_definitions[topic].ApplyMessage(input_msg: message);
        }

        
    }
}



        // static string Array2String<T>( IEnumerable<T> list )
        // {
        // return "[" + string.Join(",",list) + "]";
        // }
        

            // string msg = System.Text.Encoding.UTF8.GetString(message);

                            //CO = new ConfigObject(message)
                //foreach (ConfigObject co in CO)
                //    topics_to_open.Add(new TopicOpener(CO.topic, CO.msg_type) );
                // Application.Quit();

            // // // Supply the state information required by the task.
            // // // Create and start a thread to execute the task
            // // MyClass cls = new MyClass("stringy", 42);
            // // Thread t = new Thread(new ThreadStart(cls.MyThreadFunction));
            // // t.Start();

            // //Topic top = new Topic(config_file_topic_details); //done b4
            // ...
            // Thread t = new Thread(new ThreadStart(top.ApplyMessage)); //ino will already be in Topic class
            // t.Start();
            // return;

