using System;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using System.Reflection;
using VRViz.Messages;

namespace VRViz.Communications
{
//    public class VRVizLogger{
//        private static ILogger logger = Debug.unityLogger;
//        public static String TAG_COMMS = "VRViz_Communications";
//
//        public static ILogger GetLogger(){
//            return logger;
//        }
//
//        public static void LogInfo(string tag, string msg){
//            logger.Log(LogType.Log, tag, msg);
//        }
//    }


    public class TopicOpener {
        public TopicOpener (string ros_topic, string msg_type, VRViz.Messages.vrviz_ros.MqttParameters mqtt_param, MqttClient client) {
            string[] spl = msg_type.Split('/');

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
            }}","ros2mqtt_subscribe", ros_topic, mqtt_param.mqtt_reference.data, spl[0]+".msg:"+spl[1], mqtt_param.frequency.data, mqtt_param.latched.data ? "true" : "false", mqtt_param.qos.data);

            //Publish Request to Open Topic
            byte[] mqtt_topic_initiator = System.Text.Encoding.UTF8.GetBytes(open_message);
            string topic_opener = String.Format("__dynamic_server/topic/%s", "vrviz_ros");
            client.Publish(topic_opener, mqtt_topic_initiator, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false); 
            client.Subscribe(new string[] { mqtt_param.mqtt_reference.data }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        public static void unsubscribe(MqttClient client, string mqtt_topic) {
            client.Unsubscribe(new string[] { mqtt_topic });

        }
    }

    public class MessageDefinition{

        GameObject target_frame = null;
        object frame_component;
        System.Reflection.MethodInfo generic_method;
        Type msg_type;
        string message_target_type;
         
        public MessageDefinition(VRViz.Messages.vrviz_ros.UnityModifier unity, string msgtype){

            // Define message type
            string[] spl = msgtype.Split('/');
            this.msg_type = Type.GetType("VRViz.Messages."+spl[0]+"."+spl[1], true);
            this.message_target_type = unity.type.data;
            switch (this.message_target_type) {
                case "frame":
                    IdentifyGameObject(unity.target_frame.data);
                    IdentifyComponent(unity.component.data); break;
                case "child":
                    IdentifyGameObject(unity.target_frame.data); break;
                default: 
                    break;
            }
            //Identify topic responder: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags?view=net-5.0
            System.Reflection.BindingFlags binding_flags = BindingFlags.Static | BindingFlags.Public;
                    Debug.Log("message_definitions constructor label 5");
            this.generic_method = typeof(Modifiers).GetMethod(unity.modifier.data, binding_flags);
        }

        public void IdentifyGameObject(string frame_id) {
            this.target_frame = GameObject.Find(frame_id);
            if (this.target_frame == null) {
                this.target_frame = new GameObject(frame_id);
            }
        }

        public void IdentifyComponent(string component_id) {
            // Identify component type: https://stackoverflow.com/a/1044474
            Type generic_type = Type.GetType(component_id, true);
            this.frame_component = this.target_frame.GetComponent(generic_type);
            if (this.frame_component == null) {
                this.target_frame.AddComponent(generic_type);
                this.frame_component = this.target_frame.GetComponent(generic_type);
            }
        }

        public void ApplyMessage(byte[] input_msg) {
            string msg = System.Text.Encoding.UTF8.GetString(input_msg);
            object[] args = CreateArgs(msg);
            foreach(object o in args) Debug.Log(o);
            this.generic_method.Invoke(null, args);
        }

        public object[] CreateArgs(string msg) {
            var json = JsonConvert.DeserializeObject(msg, this.msg_type);
            switch(this.message_target_type){
                case "frame":
                    return new object[] { json, this.frame_component };
                case "child":
                    return new object[] { json, this.target_frame };
                case "data":
                    return new object[] { json };
                default:
                    Debug.LogError("Message Target Type "+ message_target_type + "is not valid.");
                    return null;
            }
        }
    }
}
