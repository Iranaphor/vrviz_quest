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

        public static void SetOdom(nav_msgs::Odometry json, Transform tf) { SetPose(json.pose.pose, tf); }
        public static void SetPoseStamped(geometry_msgs::PoseStamped json, Transform tf) { SetPose(json.pose, tf); }

        public static void SetPosition(geometry_msgs::Point json, Transform tf) {
            tf.localPosition = new Vector3((float)json.x.data, (float)0, (float)json.y.data);
        }
        public static void SetPosition(geometry_msgs::Vector3 json, Transform tf) {
            tf.localPosition = new Vector3((float)json.x.data, (float)0, (float)json.y.data);
        }

        public static void SetTruePosition(geometry_msgs::Vector3 json, Transform tf) {
            tf.localPosition = new Vector3((float)json.x.data, (float)json.z.data, (float)json.y.data);
        }

        public static void SetOrientation(geometry_msgs::Quaternion json, Transform tf) {
            Vector3 pos = new Quaternion((float)json.x.data, (float)json.y.data, (float)json.z.data, (float)json.w.data).eulerAngles; //Format Data
            tf.localRotation = Quaternion.Euler(0,90-pos.z,0); //Apply transforms
        }

        public static void SetTrueOrientation(geometry_msgs::Quaternion json, Transform tf) { //TODO: this
            Vector3 pos = new Quaternion((float)json.x.data, (float)json.y.data, (float)json.z.data, (float)json.w.data).eulerAngles; //Format Data
            tf.localRotation = Quaternion.Euler(0,90-pos.z,0); //Apply transforms
        }

        public static void SetPose(geometry_msgs::Pose json, Transform tf) {
            SetPosition(json.position, tf);
            SetOrientation(json.orientation, tf);
        }

        public static void SetImage(sensor_msgs::Image json, RawImage rgb_panel) {
            byte[] col = ImageUtils.ColourSwap(json);
            Texture2D tex = new Texture2D((int)json.width.data, (int)json.height.data, TextureFormat.RGB24, false);
            tex.LoadRawTextureData(col);
            tex.Apply();
            rgb_panel.texture = tex;
        }

        public static void SetImageC(sensor_msgs::CompressedImage json, RawImage rgb_panel) {
            byte[] col = ImageUtils.ColourSwap(json);
            Texture2D tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
            tex.LoadRawTextureData(col);
            tex.Apply();
            rgb_panel.texture = tex;
        }

        public static void SetTransformOfFrame(geometry_msgs::Transform json, GameObject target){
            Transform tf = target.transform;
            SetTruePosition(json.translation, tf);
            SetTrueOrientation(json.rotation, tf);
        }

        public static void SetImageMaterial(sensor_msgs::Image json, Renderer renderer){
            Material m = renderer.material; 
            byte[] col = ImageUtils.ColourSwap(json);
            Texture2D tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
            tex.LoadRawTextureData(col);
            m.mainTexture = tex;
        }

	}
}