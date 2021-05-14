/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using VRViz.pocso;
using VRViz.pocso.odom;
using VRViz.pocso.image;
using VRViz.pocso.compressed_image;
using VRViz.pocso.posestamped;

using sensor = vrviz.msg.sensor_msgs;
using Newtonsoft.Json;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{

    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class M2MqttUnityTest : M2MqttUnityClient
    {
        [Header("COLOURS")]
        public int RED;
        public int GREEN;
        public int BLUE;

        [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        public bool autoTest = false;
        [Header("User Interface")]
        public InputField consoleInputField;
        public Toggle encryptedToggle;
        public InputField addressInputField;
        public InputField portInputField;
        public Button connectButton;
        public Button disconnectButton;
        public Button testPublishButton;
        public Button clearButton;

        public bool once = true;
        private List<string> eventMessages = new List<string>();
        private bool updateUI = false;                       
        
        private static string[] vrviz_odom = new string[]{"__dynamic_server", "/odom","vrviz/odom", "nav_msgs.msg:Odometry", "20"};
        private static string[] vrviz_move_base_current_goal = new string[]{"__dynamic_server", "/move_base/current_goal","vrviz/move_base/current_goal", "geometry_msgs.msg:PoseStamped", "1"};
        private static string[] vrviz_camera_rgb_image_raw = new string[]{"__dynamic_server", "/camera/rgb/image_raw","vrviz/camera/rgb/image_raw", "sensor_msgs.msg:Image", "1"};
        // private static string[] vrviz_camera_rgb_image_raw_compressed = new string[]{"__dynamic_server", "/camera/rgb/image_raw/compressed","vrviz/camera/rgb/image_raw/compressed", "sensor_msgs.msg:CompressedImage", "1"};
        // private static string[] vrviz_camera_depth_image_raw = new string[]{"__dynamic_server", "/camera/depth/image_raw","vrviz/camera/depth/image_raw", "sensor_msgs.msg:Image", "1"};

        private List<string[]> topics = new List<string[]> { vrviz_odom, vrviz_move_base_current_goal };
        
        [Header("Robot Model")]
        public Transform robot_transform;
        [Header("Robot Image View")]
        public GameObject rgb_panel;
        [Header("Navigation Target")]
        public Transform goal_transform;

        public void TestPublish()
        {
            client.Publish("M2MQTT_Unity/test", System.Text.Encoding.UTF8.GetBytes("Test message"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            AddUiMessage("Test message published.");
        }

        public void SetBrokerAddress(string brokerAddress)
        {
            if (addressInputField && !updateUI)
            {
                this.brokerAddress = brokerAddress;
            }
        }

        public void SetBrokerPort(string brokerPort)
        {
            if (portInputField && !updateUI)
            {
                int.TryParse(brokerPort, out this.brokerPort);
            }
        }

        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }

        public void SetUiMessage(string msg)
        {
            if (consoleInputField != null)
            {
                consoleInputField.text = msg;
                updateUI = true;
            }
        }

        public void AddUiMessage(string msg)
        {
            if (consoleInputField != null)
            {
                consoleInputField.text += msg + "\n";
                updateUI = true;
            }
        }

        protected override void OnConnecting()
        {
            base.OnConnecting();
            SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            SetUiMessage("Connected to broker on " + brokerAddress + "\n");

            if (autoTest)
            {
                TestPublish();
            }
        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            AddUiMessage("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            AddUiMessage("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            AddUiMessage("CONNECTION LOST!");
        }

        private void UpdateUI()
        {
            if (client == null)
            {
                if (connectButton != null)
                {
                    connectButton.interactable = true;
                    disconnectButton.interactable = false;
                    testPublishButton.interactable = false;
                }
            }
            else
            {
                if (testPublishButton != null)
                {
                    testPublishButton.interactable = client.IsConnected;
                }
                if (disconnectButton != null)
                {
                    disconnectButton.interactable = client.IsConnected;
                }
                if (connectButton != null)
                {
                    connectButton.interactable = !client.IsConnected;
                }
            }
            if (addressInputField != null && connectButton != null)
            {
                addressInputField.interactable = connectButton.interactable;
                addressInputField.text = brokerAddress;
            }
            if (portInputField != null && connectButton != null)
            {
                portInputField.interactable = connectButton.interactable;
                portInputField.text = brokerPort.ToString();
            }
            if (encryptedToggle != null && connectButton != null)
            {
                encryptedToggle.interactable = connectButton.interactable;
                encryptedToggle.isOn = isEncrypted;
            }
            if (clearButton != null && connectButton != null)
            {
                clearButton.interactable = connectButton.interactable;
            }
            updateUI = false;
        }

        protected override void Start()
        {
            SetUiMessage("Ready.");
            base.Start();
        }

        protected override void Update()
        {
            base.Update(); // call ProcessMqttEvents()
            if (updateUI) { UpdateUI(); }
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void OnValidate()
        {
            if (autoTest)
            {
                autoConnect = true;
            }
        }

        protected override void UnsubscribeTopics()
        {
            foreach (string[] t in topics)
            {
                string factory = t[0];
                string topic = factory;

                if (factory == "__dynamic_server") 
                {
                    string mqtt_topic = t[2];
                    topic = mqtt_topic;
                }
                client.Unsubscribe(new string[] { topic });
                Debug.Log("Unsubscribed from Subscriber: " + topic);
            }
        }







        protected void OpenTopic(string ros_topic, string mqtt_reference, string msg_type, int frequency = 1, bool latched = false, int qos = MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE) {
            /*
            1. send to control topic /dynamic_server/topic/# ros2mqtt message. i.e. tell ros to forward topic X to MQTT
            2. listen to mqtt topic on quest
            
            Args: //TODO: complete this
                @mqtt_reference: the topic to listen to on the mqtt cloud
                @ros_topic: the topic on the roscore to request access to
                @msg_type: the format of the message
                @frequency: ?
                @latched: ?
                @qos: ?
            */

            //Format message to initiate topic
            StringBuilder sb = new StringBuilder(1024);
            sb.Append("{");
            sb.AppendFormat("'op': '{0}', ", "ros2mqtt_subscribe");
                sb.Append("'args':{ ");
                    sb.AppendFormat("'topic_from':'{0}', ", ros_topic);
                    sb.AppendFormat("'topic_to':'{0}', ", mqtt_reference);
                    sb.AppendFormat("'msg_type':'{0}', ", msg_type);
                    sb.AppendFormat("'frequency':{0}, ", frequency); //TODO: this doesnt accept Non-type Int
                    sb.AppendFormat("'latched':{0}, ", "false"); //TODO: fix this
                    sb.AppendFormat("'qos':{0} ", qos);
                sb.Append("}");
            sb.Append("}");
            string string_to_pub = @sb.ToString().Replace("'","\"");

            byte[] mqtt_topic_initiator = System.Text.Encoding.UTF8.GetBytes(string_to_pub);
            Debug.Log("Request to open: \n\n  " + string_to_pub + "  \n\n");



            //Publish message to MQTT Dynamic Server
            string topic_opener = String.Format("__dynamic_server/topic/%s", "vrviz_ros");
            //TODO: check this is the correct qos (we need qos:2)
            //  There are 3 QoS levels in MQTT:
            //     At most once (0)
            //     At least once (1)
            //     Exactly once (2).
            client.Publish(topic_opener, mqtt_topic_initiator, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false); 

            Debug.Log(String.Format("Publishing request to open {0} accessible through {1}.", new object[] {ros_topic, mqtt_reference} ));
        }


        protected override void SubscribeTopics()
        {
            //TODO change msg_type to variable!!!!
            foreach (string[] t in topics)
            {
                string factory = t[0];
                string topic = factory;

                if (factory == "__dynamic_server")
                {
                    string ros_topic = t[1];
                    string mqtt_topic = t[2];
                    string msg_type = t[3];
                    int frequency = Int32.Parse(t[4]);
                    topic = mqtt_topic;
                    
                    //Identify topic to publish to
                    OpenTopic(ros_topic: ros_topic, 
                              mqtt_reference: mqtt_topic, 
                              msg_type: msg_type,
                              frequency: frequency,
                              latched: false,
                              qos: MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE);

                }
                client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                Debug.Log("Created Subscriber to: " + topic);
            }
        }


        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            
            switch (topic)
            {
                case "vrviz/odom":
                    ODOM_Message json_parsed_odom = JsonUtility.FromJson<ODOM_Message>(msg);
                    SetPose(robot_transform, json_parsed_odom.pose);
                    break;

                case "vrviz/move_base/current_goal":
                    POSE_STAMPED_Message json_parsed_goal = JsonUtility.FromJson<POSE_STAMPED_Message>(msg);
                    SetPosition(goal_transform, json_parsed_goal.pose.position);
                    break;

                // case "vrviz/camera/rgb/image_raw/compressed":
                    // COMPRESSED_IMAGE_Message json_parsed_compressed_image = JsonUtility.FromJson<COMPRESSED_IMAGE_Message>(msg);
                    // client.Unsubscribe(new string[] { topic });
                    // SetImage(rgb_panel, 480, 640, json_parsed_compressed_image.data);
                    // break;

                case "vrviz/camera/rgb/image_raw":
                    IMAGE_Message json_parsed_image = JsonUtility.FromJson<IMAGE_Message>(msg);
                    SetImagePOSCO(rgb_panel, json_parsed_image);
                    //https://www.reddit.com/r/Unity3D/comments/3pxim1/loading_textures_from_diskweb_without_hiccuping/cwadfm1/

                    /*
                    // sensor.Image parsed_image = JsonConvert.DeserializeObject<sensor.Image>(msg);
                    // client.Unsubscribe(new string[] { topic });
                    if(once){
                        once=!once;
                        Debug.LogWarning(parsed_image.is_bigendian.data); // -> "MSG"
                    }
                    //*/
                    break;

                default:
                    break;
            }
        }

        protected void SetPosition(Transform tf, Position pos)
        {
            tf.localPosition = new Vector3(pos.x, 0, pos.y); //Apply transforms
        }
        protected void SetOrientation(Transform tf, Orientation orient)
        {
            Vector3 pos = new Quaternion(orient.x, orient.y, orient.z, orient.w).eulerAngles; //Format Data
            tf.localRotation = Quaternion.Euler(0,90-pos.z,0); //Apply transforms

        }
        protected void SetPose(Transform tf, CovariancePose pose)
        {
            SetPosition(tf, pose.pose.position);
            SetOrientation(tf, pose.pose.orientation);
        }
        protected void SetImage(GameObject rgb_panel, int width, int height, string data)
        {
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            
            Color32[] colorArray = new Color32[data.Length/4];
            byte c1;
            byte c2;
            byte c3;
            byte c4 = (byte) 255;
            
            for(var i = 0; i < data.Length; i+=4)
            {
                c1 = (byte) data[i+RED];
                c2 = (byte) data[i+GREEN];
                c3 = (byte) data[i+BLUE];
                Color32 color = new Color32(c1, c2, c3, c4);
                colorArray[i/4] = color;
            }
            tex.SetPixels32(colorArray);
            tex.Apply();
            rgb_panel.GetComponent<RawImage>().texture = tex;
        }
        protected void SetImagePOSCO(GameObject rgb_panel, IMAGE_Message json)
        {

            Texture2D tex = new Texture2D((int)json.width, (int)json.height, TextureFormat.RGBA32, false);
            
            Color32[] colorArray = new Color32[json.data.Length/4];
            byte c1;
            byte c2;
            byte c3;
            byte c4 = (byte) 255;
            
            //320 == BAGR32

            for(var i = 0; i < json.data.Length; i+=4)
            {
                c1 = (byte) json.data[i+RED];
                c2 = (byte) json.data[i+GREEN];
                c3 = (byte) json.data[i+BLUE];
                // c4 = (byte) json.data[i+3];
                
                Color32 color = new Color32(c1, c2, c3, c4);
                colorArray[i/4] = color;
            }
            tex.SetPixels32(colorArray);

            // tex.LoadRawTextureData(Encoding.ASCII.GetBytes( json.data ) );//MAYBE
            // tex.LoadRawTextureData(Encoding.Default.GetBytes( json.data ) );//MAYBE
            // tex.LoadRawTextureData(Encoding.UTF8.GetBytes( json.data ) );//MAYBE
            tex.Apply();
            rgb_panel.GetComponent<RawImage>().texture = tex;
            // Debug.LogError(rgb_panel.uvRect);
        }
        
    }
}
