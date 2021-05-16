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
using System.Timers;
using System.Collections;
using System.Collections.Generic;
using StopWatch = System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

using TopicOpener = VRViz.Communications.TopicOpener;

using std_msgs = VRViz.Messages.std_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;
using sensor_msgs = VRViz.Messages.sensor_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class M2MqttUnityNew : M2MqttUnityTest
    {

        [Header("COLOURS")]
        public int RED;
        public int GREEN;
        public int BLUE;

        [Header("Topic Modifiers")]
        private static List<TopicOpener> topics_to_open = new List<TopicOpener>();

        [Header("Odometry")]
        public Transform robot_transform;
        private TopicOpener vrviz_odom = new TopicOpener(ros_topic:"/odom", msg_type:nav_msgs::Odometry.ToRosString(), frequency:20);

        [Header("Image Raw")]
        public GameObject rgb_panel;
        private TopicOpener vrviz_camera_rgb_image_raw = new TopicOpener(ros_topic:"/camera/rgb/image_raw", msg_type:sensor_msgs::Image.ToRosString());
        private Texture2D tex;
        private Boolean tex_once = True;

        [Header("Goal")]
        public Transform goal_transform;
        private TopicOpener vrviz_move_base_current_goal = new TopicOpener(ros_topic:"/move_base/current_goal", msg_type:geometry_msgs::PoseStamped.ToRosString(), latched:true);
            
        [Header("Path")]
        // public GameObject path_collection;
        private TopicOpener vrviz_move_base_navfnros_plan = new TopicOpener(ros_topic:"/move_base/NavfnROS/plan", msg_type:nav_msgs::Path.ToRosString(), latched:true);
            

        
        protected override void SubscribeTopics() { foreach (TopicOpener o in topics_to_open) o.open_topic(client);}
        protected override void Start() {
            topics_to_open.Add(vrviz_odom);
            topics_to_open.Add(vrviz_camera_rgb_image_raw);
            topics_to_open.Add(vrviz_move_base_current_goal);
            // topics_to_open.Add(vrviz_move_base_navfnros_plan);
            base.Start();
        }



        protected override void DecodeMessage(string topic, byte[] message) {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            switch (topic) {
                case "vrviz/odom":  // More accurate, less active: /move_base/feedback/feedback/pose
                    nav_msgs::Odometry json1 = JsonConvert.DeserializeObject<nav_msgs::Odometry>(msg);
                    SetPose(robot_transform, json1.pose.pose);
                    break;

                case "vrviz/move_base/current_goal":
                    geometry_msgs::PoseStamped json2 = JsonConvert.DeserializeObject<geometry_msgs::PoseStamped>(msg);
                    SetPosition(goal_transform, json2.pose.position);
                    break;

                case "vrviz/camera/rgb/image_raw":
                    // client.Unsubscribe(new string[] { topic });
                    sensor_msgs::Image json3 = JsonConvert.DeserializeObject<sensor_msgs::Image>(msg);
                    if (tex_once) { tex_once = false;  this.tex = new Texture2D((int)json3.width.data, (int)json3.height.data, TextureFormat.RGBA32, false); }
                    SetImage(rgb_panel, json3.data, this.tex);
                    break;

                case "/move_base/NavfnROS/plan":
                    Debug.LogError(msg);
                    nav_msgs::Path json4 = JsonConvert.DeserializeObject<nav_msgs::Path>(msg);
                    break;

                default:
                    break;
            }
        }

        protected void SetPosition(Transform tf, geometry_msgs::Point pos) {
            tf.localPosition = new Vector3((float)pos.x.data, (float)0, (float)pos.y.data); //Apply transforms
        }

        protected void SetOrientation(Transform tf, geometry_msgs::Quaternion orient) {
            Vector3 pos = new Quaternion((float)orient.x.data, (float)orient.y.data, (float)orient.z.data, (float)orient.w.data).eulerAngles; //Format Data
            tf.localRotation = Quaternion.Euler(0,90-pos.z,0); //Apply transforms

        }

        protected void SetPose(Transform tf, geometry_msgs::Pose pose) {
            SetPosition(tf, pose.position);
            SetOrientation(tf, pose.orientation);
        }

        protected void SetImage(GameObject rgb_panel, std_msgs::UInt8[] data, Texture2D tex) {
            //https://www.reddit.com/r/Unity3D/comments/3pxim1/loading_textures_from_diskweb_without_hiccuping/cwadfm1/
            // StopWatch.Stopwatch stopwatch0 = StopWatch.Stopwatch.StartNew(); 

            // StopWatch.Stopwatch stopwatch1b = StopWatch.Stopwatch.StartNew(); 
            byte[] col = new byte[data.Length];
            // stopwatch1b.Stop();

            // StopWatch.Stopwatch stopwatch1c = StopWatch.Stopwatch.StartNew(); 
            for (int i = 0; i < data.Length; i+=4)
            {
                col[i+0] = data[i+RED].data;
                col[i+1] = data[i+GREEN].data;
                col[i+2] = data[i+BLUE].data;
                col[i+3] = (byte)255;
            }
            // stopwatch1c.Stop();

            // StopWatch.Stopwatch stopwatch2 = StopWatch.Stopwatch.StartNew(); 
            tex.LoadRawTextureData(col);
            // stopwatch2.Stop();

            // StopWatch.Stopwatch stopwatch3 = StopWatch.Stopwatch.StartNew(); 
            tex.Apply();
            // stopwatch3.Stop();

            // StopWatch.Stopwatch stopwatch4 = StopWatch.Stopwatch.StartNew(); 
            rgb_panel.GetComponent<RawImage>().texture = tex;
            // stopwatch4.Stop();

            // stopwatch0.Stop();
            // Debug.Log("Stopwatches:");
            // Debug.Log(stopwatch0.ElapsedMilliseconds);
            // Debug.Log(stopwatch1b.ElapsedMilliseconds);
            // Debug.Log(stopwatch1c.ElapsedMilliseconds);
            // Debug.Log(stopwatch2.ElapsedMilliseconds);
            // Debug.Log(stopwatch3.ElapsedMilliseconds);
            // Debug.Log(stopwatch4.ElapsedMilliseconds);
            // Debug.Log("Stopwatches End.");

        }
        
        public float scale_byte(byte bin){
            return Convert.ToSingle(bin)/255;
        }

        

    }

    

}
