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
using sensor_msgs = vrviz.msg.sensor_msgs;

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
        
        private static string[] example = new string[]{"M2MQTT_Unity/test","",""};
        private static string[] vrviz_camera_rgb_image_raw = new string[]{"__dynamic_server", "/camera/rgb/image_raw","vrviz/camera/rgb/image_raw", "sensor_msgs.msg:Image", "1"};
        private static string[] vrviz_camera_depth_image_raw = new string[]{"__dynamic_server", "/camera/depth/image_raw","vrviz/camera/depth/image_raw", "sensor_msgs.msg:Image", "1"};
        private static string[] odom = new string[]{"__dynamic_server", "/odom","vrviz/odom", "nav_msgs.msg:Odometry", "5"};

        private List<string[]> topics = new List<string[]> { odom, vrviz_camera_rgb_image_raw };
        
        [Header("Robot Model")]
        public Transform robot_transform;
        public GameObject rgb_panel;

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
                              frequency: 1,
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
                    SetPosition(robot_transform, json_parsed_odom);
                    break;
                case "vrviz/camera/rgb/image_raw":
                    sensor_msgs::Image json_parsed_image = JsonUtility.FromJson<sensor_msgs::Image>(msg);
                    client.Unsubscribe(new string[] { topic });
                    
                    if(once){
                        once=!once;
                        Debug.LogError("MSG"); // -> "MSG"
                        // Debug.LogError(json_parsed_image); // -> "vrviz.msg.sensor_msg.Image"
                        Debug.LogError(json_parsed_image.header.frame_id.data);// -> "System.Byte[]"
                        Debug.LogError(json_parsed_image.width.data); 
                        Debug.LogError(json_parsed_image.data.Length);  // -> "0" 1228800/(640*480) => 4
                        SetImage(rgb_panel, json_parsed_image);
                    }

                    break;
                default:
                    break;
            }
        }

        protected void SetPosition(Transform tf, ODOM_Message json)
        {
            //Format Data
            Position parse = json.pose.pose.position;
            Orientation orient = json.pose.pose.orientation;
            Vector3 pos = new Quaternion(orient.x, orient.y, orient.z, orient.w).eulerAngles;

            //Apply transforms
            tf.localPosition = new Vector3(parse.x, 0, parse.y);
            tf.localRotation = Quaternion.Euler(0,90-pos.z,0);
        }
        protected void SetImage(GameObject rgb_panel, sensor_msgs::Image json)
        {
            // Texture2D tex = new Texture2D((int)json.width.data, (int)json.height.data);
            Texture2D tex = new Texture2D((int)json.width.data, (int)json.height);
            Debug.LogError("W:");
            Debug.LogError(tex.width);
            Debug.LogError("H:");
            Debug.LogError(tex.height);
            
            
            Color32[] colorArray = new Color32[json.data.Length/4];
            for(var i = 0; i < json.data.Length; i+=4)
            {
                Color32 color = new Color32((byte)json.data[i + 2], (byte)json.data[i + 1], (byte)json.data[i + 0], (byte)json.data[i + 3]);
                colorArray[i/4] = color;
            }
            Debug.LogError(colorArray.Length);
            tex.SetPixels32(colorArray);

            // https://docs.unity3d.com/ScriptReference/Texture2D.LoadRawTextureData.html
            
            // tex.LoadRawTextureData( Encoding.ASCII.GetBytes(json.data) );
            // tex.LoadRawTextureData(json.byteData);

            /*
            byte[] pvrtcBytes = new byte[]
            {
                0x30, 0x32, 0x32, 0x32, 0xe7, 0x30, 0xaa, 0x7f, 0x32, 0x32, 0x32, 0x32, 0xf9, 0x40, 0xbc, 0x7f,
                0x03, 0x03, 0x03, 0x03, 0xf6, 0x30, 0x02, 0x05, 0x03, 0x03, 0x03, 0x03, 0xf4, 0x30, 0x03, 0x06,
                0x32, 0x32, 0x32, 0x32, 0xf7, 0x40, 0xaa, 0x7f, 0x32, 0xf2, 0x02, 0xa8, 0xe7, 0x30, 0xff, 0xff,
                0x03, 0x03, 0x03, 0xff, 0xe6, 0x40, 0x00, 0x0f, 0x00, 0xff, 0x00, 0xaa, 0xe9, 0x40, 0x9f, 0xff,
                0x5b, 0x03, 0x03, 0x03, 0xca, 0x6a, 0x0f, 0x30, 0x03, 0x03, 0x03, 0xff, 0xca, 0x68, 0x0f, 0x30,
                0xaa, 0x94, 0x90, 0x40, 0xba, 0x5b, 0xaf, 0x68, 0x40, 0x00, 0x00, 0xff, 0xca, 0x58, 0x0f, 0x20,
                0x00, 0x00, 0x00, 0xff, 0xe6, 0x40, 0x01, 0x2c, 0x00, 0xff, 0x00, 0xaa, 0xdb, 0x41, 0xff, 0xff,
                0x00, 0x00, 0x00, 0xff, 0xe8, 0x40, 0x01, 0x1c, 0x00, 0xff, 0x00, 0xaa, 0xbb, 0x40, 0xff, 0xff,
            };
            //GetComponent<Renderer>().material.mainTexture = tex;
            */

            tex.Apply();
            rgb_panel.GetComponent<RawImage>().texture = tex;
        }
        
    }
}
