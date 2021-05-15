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
    public class M2MqttUnityNew : M2MqttUnityTest
    {

        [Header("COLOURS")]
        public int RED;
        public int GREEN;
        public int BLUE;

        [Header("Topic Modifiers")]
        private bool placeholder;

        [Header("Odometry")]
        public Transform robot_transform;
        private static string[] vrviz_odom = new string[]{"__dynamic_server", "/odom","vrviz/odom", "nav_msgs.msg:Odometry", "20"};

        [Header("Image Raw")]
        public GameObject rgb_panel;
        private static string[] vrviz_camera_rgb_image_raw = new string[]{"__dynamic_server", "/camera/rgb/image_raw","vrviz/camera/rgb/image_raw", "sensor_msgs.msg:Image", "1"};

        [Header("Goal")]
        public Transform goal_transform;
        private static string[] vrviz_move_base_current_goal = new string[]{"__dynamic_server", "/move_base/current_goal","vrviz/move_base/current_goal", "geometry_msgs.msg:PoseStamped", "1"};

        
        // private static string[] vrviz_camera_rgb_image_raw_compressed = new string[]{"__dynamic_server", "/camera/rgb/image_raw/compressed","vrviz/camera/rgb/image_raw/compressed", "sensor_msgs.msg:CompressedImage", "1"};
        // private static string[] vrviz_camera_depth_image_raw = new string[]{"__dynamic_server", "/camera/depth/image_raw","vrviz/camera/depth/image_raw", "sensor_msgs.msg:Image", "1"};
        

        protected override void Start() {
            this.topics = new List<string[]> { vrviz_odom, vrviz_move_base_current_goal, vrviz_camera_rgb_image_raw };
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message) {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            
            switch (topic) {
                case "vrviz/odom":
                    ODOM_Message json_parsed_odom = JsonUtility.FromJson<ODOM_Message>(msg);
                    SetPose(robot_transform, json_parsed_odom.pose);
                    break;

                case "vrviz/move_base/current_goal":
                    POSE_STAMPED_Message json_parsed_goal = JsonUtility.FromJson<POSE_STAMPED_Message>(msg);
                    SetPosition(goal_transform, json_parsed_goal.pose.position);
                    break;

                case "vrviz/camera/rgb/image_raw":
                    IMAGE_Message json_image = JsonUtility.FromJson<IMAGE_Message>(msg);
                    SetImage(rgb_panel, (int)json_image.width, (int)json_image.height, json_image.data);

                    sensor.Image parsed_image = JsonConvert.DeserializeObject<sensor.Image>(msg);
                    if(once){
                        once=!once;
                        Debug.LogError(parsed_image.width.data);
                    }
                    

                    // client.Unsubscribe(new string[] { topic });
                    break;

                default:
                    break;
            }
        }




        protected void SetPosition(Transform tf, Position pos) {
            tf.localPosition = new Vector3(pos.x, 0, pos.y); //Apply transforms
        }

        protected void SetOrientation(Transform tf, Orientation orient) {
            Vector3 pos = new Quaternion(orient.x, orient.y, orient.z, orient.w).eulerAngles; //Format Data
            tf.localRotation = Quaternion.Euler(0,90-pos.z,0); //Apply transforms

        }

        protected void SetPose(Transform tf, CovariancePose pose) {
            SetPosition(tf, pose.pose.position);
            SetOrientation(tf, pose.pose.orientation);
        }

        protected void SetImage(GameObject rgb_panel, int width, int height, string data) {
            //https://www.reddit.com/r/Unity3D/comments/3pxim1/loading_textures_from_diskweb_without_hiccuping/cwadfm1/
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            
            Color32[] colorArray = new Color32[data.Length/4];
            byte c1;
            byte c2;
            byte c3;
            byte c4 = (byte) 255;
            
            for(var i = 0; i < data.Length; i+=4) {
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
        
    }
}
