using System;
using System.Collections;
using System.Collections.Generic;
using StopWatch = System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Timers;

using UnityEngine;
using UnityEngine.UI;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

using TopicOpener = VRViz.Communications.TopicOpener;
using Modifiers = VRViz.Modifiers;

using Messages = VRViz.Messages;
using Odometry = VRViz.Messages.nav_msgs.Odometry;
using std_msgs = VRViz.Messages.std_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;
using sensor_msgs = VRViz.Messages.sensor_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;

namespace M2MqttUnity.Examples
{
    public class M2MqttUnityNew : M2MqttUnityTest
    {

        [Header("COLOURS")]
        public int RED;
        public int GREEN;
        public int BLUE;
        private bool once = true;

        // [Header("Config Downloader")]
        // private TopicOpener vrviz_vrviz_config = new TopicOpener(ros_topic:"/vrviz/config", msg_type:std_msgs::String.ToRosString(), latched:true);

        [Header("Topic Modifiers")]
        private static List<TopicOpener> topics_to_open = new List<TopicOpener>();
        // private TopicOpener vrviz_camera_rgb_image_raw_compressed =     new TopicOpener(ros_topic:"/camera/rgb/image_raw/compressed",    msg_type:sensor_msgs::CompressedImage.ToRosString(), frequency:1);
        private TopicOpener vrviz_camera_rgb_image_raw =     new TopicOpener(ros_topic:"/camera/rgb/image_raw",    msg_type:sensor_msgs::Image.ToRosString(), frequency:0.1);
        private TopicOpener vrviz_move_base_current_goal =   new TopicOpener(ros_topic:"/move_base/current_goal",  msg_type:geometry_msgs::PoseStamped.ToRosString(), latched:true);
        private TopicOpener vrviz_move_base_navfnros_plan =  new TopicOpener(ros_topic:"/move_base/NavfnROS/plan", msg_type:nav_msgs::Path.ToRosString(), latched:true);
        private TopicOpener vrviz_odom =                     new TopicOpener(ros_topic:"/odom",                    msg_type:nav_msgs::Odometry.ToRosString(), frequency:20);
        private TopicOpener vrviz_scan =                     new TopicOpener(ros_topic:"/scan",                    msg_type:sensor_msgs::LaserScan.ToRosString());    

        
        protected override void SubscribeTopics() { foreach (TopicOpener o in topics_to_open) o.open_topic(client);}
        protected override void Start() {
            // topics_to_open.Add(vrviz_vrviz_config);

            // Debug.Log(Assembly.GetExecutingAssembly().GetName().Name);
            // Debug.Log(Assembly.GetAssembly(typeof(GameObject)).GetName().Name);
            // Debug.Log(Assembly.GetAssembly(typeof(RawImage)).GetName().Name);
            // Debug.Log(typeof(RawImage).AssemblyQualifiedName);

            topics_to_open.Add(vrviz_camera_rgb_image_raw);
            // topics_to_open.Add(vrviz_camera_rgb_image_raw_compressed);
            topics_to_open.Add(vrviz_move_base_current_goal);
            // topics_to_open.Add(vrviz_move_base_navfnros_plan);
            topics_to_open.Add(vrviz_odom);
            // topics_to_open.Add(vrviz_scan);
            base.Start();
        }


        public static void ApplyMessage(string topic, string input_gameobject, string input_component, string input_modifier, byte[] input_msg, string input_msgtype) { //, string parent) {

            //Decode message
            string msg = System.Text.Encoding.UTF8.GetString(input_msg);
            Type msg_type = Type.GetType(input_msgtype, true);
            var json = JsonConvert.DeserializeObject(msg, msg_type);


            // Identify GameObject
            GameObject generic_gameobject = GameObject.Find(input_gameobject);
            if (generic_gameobject == null) {
                generic_gameobject = new GameObject(input_gameobject);
            }


            // Define Component Type /* https://stackoverflow.com/a/1044474 */
            Type generic_type = Type.GetType(input_component, true);


            // Get Component
            var generic_component = generic_gameobject.GetComponent(generic_type);
            if (generic_component == null) {
                generic_gameobject.AddComponent(generic_type);
                generic_component = generic_gameobject.GetComponent(generic_type);
                //TODO: add explicit parent for new gameobjects (passed in as arg)
            }


            // Properties of method  /* https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags?view=net-5.0 */
            System.Reflection.BindingFlags binding_flags = BindingFlags.Static | BindingFlags.Public;
            
            //Call method to modify Component
            System.Reflection.MethodInfo generic_method = typeof(Modifiers).GetMethod(input_modifier, binding_flags);
            generic_method.Invoke(null, new object[]{ generic_component, json} );
        }
        


        protected override void DecodeMessage(string topic, byte[] message) {  // client.Unsubscribe(new string[] { topic });
            string msg = System.Text.Encoding.UTF8.GetString(message);

            if (topic == "vrviz/config") {
                //CO = new ConfigObject(message)
                //foreach (ConfigObject co in CO)
                //    topics_to_open.Add(new TopicOpener(CO.topic, CO.msg_type) );

                // Application.Quit();
            }



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


            // Thread t = new Thread(new ThreadStart(ApplyMessage));
            switch (topic) {
                case "vrviz/odom":  // More accurate, less active: /move_base/feedback/feedback/pose
                    ApplyMessage(topic, "RobotModel", "UnityEngine.Transform, UnityEngine.CoreModule", "SetOdom", message, "VRViz.Messages.nav_msgs.Odometry");
                    // t.Start();
                    break;

                case "vrviz/move_base/current_goal":
                    ApplyMessage(topic, "GoalMarker", "UnityEngine.Transform, UnityEngine.CoreModule", "SetPoseStamped", message, "VRViz.Messages.geometry_msgs.PoseStamped");
                    break;

                case "vrviz/camera/rgb/image_raw":
                    ApplyMessage(topic, "RGBPanel", "UnityEngine.UI.RawImage, UnityEngine.UI", "SetImage", message, "VRViz.Messages.sensor_msgs.Image");
                    break;

                case "vrviz/camera/rgb/image_raw/compressed":
                    // ApplyMessage(topic, "RGBPanelCompressed", "UnityEngine.UI.RawImage, UnityEngine.UI", "SetImageC", message, "VRViz.Messages.sensor_msgs.CompressedImage");
                    break;

                case "vrviz/move_base/NavfnROS/plan":
                    // Debug.LogError(msg);
                    // nav_msgs::Path json4 = JsonConvert.DeserializeObject<nav_msgs::Path>(msg);
                    break;

                case "vrviz/scan":
                    // sensor_msgs::LaserScan test_json = JsonConvert.DeserializeObject<sensor_msgs::LaserScan>(msg);
                    break;

                default:
                    break;
            }
        }

        
        static string Array2String<T>( IEnumerable<T> list )
        {
        return "[" + string.Join(",",list) + "]";
        }
        

    }
    

}

