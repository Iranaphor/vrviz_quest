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

        private List<string> eventMessages = new List<string>();
        private bool updateUI = false;                       
        
        private static string[] example = new string[]{"M2MQTT_Unity/test","",""};
        private static string[] ros = new string[]{"__dynamic_server", "/test","vrviz/test", "std_msgs.msg:String"};
        private static string[] ros2 = new string[]{"__dynamic_server", "/odom","vrviz/odom", "nav_msgs.msg:Odometry"};

        private List<string[]> topics = new List<string[]> { ros2 }; //__dynamic_server_DATA__test_TO_m2_test
        
        
        [Header("Robot Model")]
        public Transform robot_transform;


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

        protected void OpenTopic(string ros_topic, string mqtt_reference, string msg_type, int frequency = 24, bool latched = false, int qos = MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE) {
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
                    //Identify topic to publish to     //__dynamic_server_DATA__test_TO_m2_test
                    // topic = factory + "_DATA_" + ros_topic + "_TO_" + mqtt_topic;
                    // topic = topic.Replace('/', '_');
                    topic = mqtt_topic;

                    //Identify topic to publish to
                    OpenTopic(ros_topic, mqtt_topic, msg_type);
                }
                client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                Debug.Log("Created Subscriber to: " + topic);
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


        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);

            if (topic == "vrviz/odom") { 
                ODOM_Message json_parsed = JsonUtility.FromJson<ODOM_Message>(msg);
                SetPosition(robot_transform, json_parsed);
            }

            // Debug.Log(topic + " received: " + msg);
            // AddUiMessage("Message received from: " + topic);
            // AddUiMessage("\" " + msg + " \"");
            // AddUiMessage("----------------------");
        }

        protected void SetPosition(Transform tf, ODOM_Message json)
        {
            //
            Position parse = json.pose.pose.position;
            Orientation orient = json.pose.pose.orientation;
            Vector3 pos = new Quaternion(orient.x, orient.y, orient.z, orient.w).eulerAngles;

            //Apply transforms
            tf.position = new Vector3(parse.x, 0, parse.y);
            tf.rotation = Quaternion.Euler(0,pos.z,0);
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
    }
}



