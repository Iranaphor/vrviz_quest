using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

using VRViz.Utilities;

using std_msgs = VRViz.Messages.std_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;
using sensor_msgs = VRViz.Messages.sensor_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;

namespace VRViz {
	public class Modifiers {

        public static void SetOdom(Transform tf, nav_msgs::Odometry odom) { SetPose(tf, odom.pose.pose); }
        public static void SetPoseStamped(Transform tf, geometry_msgs::PoseStamped ps) { SetPose(tf, ps.pose); }

        public static void SetPosition(Transform tf, geometry_msgs::Point pos) {
            tf.localPosition = new Vector3((float)pos.x.data, (float)0, (float)pos.y.data);
        }

        public static void SetOrientation(Transform tf, geometry_msgs::Quaternion orient) {
            Vector3 pos = new Quaternion((float)orient.x.data, (float)orient.y.data, (float)orient.z.data, (float)orient.w.data).eulerAngles; //Format Data
            tf.localRotation = Quaternion.Euler(0,90-pos.z,0); //Apply transforms
        }

        public static void SetPose(Transform tf, geometry_msgs::Pose pose) {
            SetPosition(tf, pose.position);
            SetOrientation(tf, pose.orientation);
        }

        public static void SetImage(RawImage rgb_panel, sensor_msgs::Image json) {
            byte[] col = ImageUtils.ColourSwap(json);
            Texture2D tex = new Texture2D((int)json.width.data, (int)json.height.data, TextureFormat.RGB24, false);
            tex.LoadRawTextureData(col);
            tex.Apply();
            rgb_panel.texture = tex;

        }

        public static void SetImageC(RawImage rgb_panel, sensor_msgs::CompressedImage json) {
            byte[] col = ImageUtils.ColourSwap(json);
            Texture2D tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
            tex.LoadRawTextureData(col);
            tex.Apply();
            rgb_panel.texture = tex;

        }


	}
}