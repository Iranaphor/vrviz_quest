using System;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace VRViz.Communications
{
    ///<summary>
    /// Sends a message to an MQTT ROS bridge server 
    ///</summary>
    public class TopicOpener {
        
        public string _control_topic;
        public string _mqtt_reference;
        public string _ros_topic;
        public string _msg_type;
        public int _frequency;
        public bool _latched;
        public int _qos;
        
        public TopicOpener (string ros_topic, string msg_type, string mqtt_reference="", int frequency=1, bool latched=false, int qos=MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, string control_topic = "__dynamic_sever") {
            this._control_topic = control_topic;
            this._mqtt_reference = mqtt_reference == "" ?  "vrviz"+ros_topic : mqtt_reference; //if no mqtt_ref given, use ros_topic
            this._ros_topic = ros_topic;
            this._msg_type = msg_type;
            this._frequency = frequency;
            this._latched = latched;
            this._qos = qos;
        }

        public string string_format() {
            return String.Format(@"
            {{""op"": ""{0}"",
             ""args"":{{ 
                    ""topic_from"":""{1}"", 
                    ""topic_to"":""{2}"", 
                    ""msg_type"":""{3}"", 
                    ""frequency"":{4}, 
                    ""latched"":{5}, 
                    ""qos"":{6}
                    }}
            }}","ros2mqtt_subscribe", this._ros_topic, this._mqtt_reference, this._msg_type, this._frequency, this._latched ? "true" : "false", this._qos);
        }

        public void open_topic(MqttClient client) {
            //Format byte[] to send topic opener information
            byte[] mqtt_topic_initiator = System.Text.Encoding.UTF8.GetBytes(this.string_format());
            
            //Publish message to MQTT Dynamic Server
            string topic_opener = String.Format("__dynamic_server/topic/%s", "vrviz_ros");
            
            client.Publish(topic_opener, mqtt_topic_initiator, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false); 
            // Debug.Log(String.Format("Publishing request to open {0} accessible through {1}.", new object[] {this._ros_topic, this._mqtt_reference} ));

            client.Subscribe(new string[] { this._mqtt_reference }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            // Debug.Log(String.Format("Subscribing to {0}.", new object[] {this._mqtt_reference} ));

        }
    }
}



/*


[WARN] [1621119341.728386, 3206.660000]: Client request (forward ros topic to mqtt): 
{u'args': {u'frequency': 1,
           u'latched': True,
           u'msg_type': u'geometry_msgs.msg:PoseStamped',
           u'qos': 2,
           u'topic_from': u'/move_base/current_goal',
           u'topic_to': u'vrviz/move_base/current_goal'},
 u'op': u'ros2mqtt_subscribe'}


*/