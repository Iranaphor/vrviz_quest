using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using VRViz.Utilities;

using std_msgs = VRViz.Messages.std_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;
using sensor_msgs = VRViz.Messages.sensor_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;

namespace VRViz.Modifiers {
	public class MessageApplications {
        public static Transform FindChild(Transform parent, string name, GameObject prefab) {
            if ((Transform)parent.Find(name) == null) {
                GameObject child_go = (GameObject)UnityEngine.Object.Instantiate(prefab, parent);
                child_go.name = name;
            }
            return parent.Find(name);
        }

        public static void SetPointCloud2(sensor_msgs::PointCloud2 json, Transform tf, string child_prefix) {
            //TODO: we must identify x,y,z from json.fields
            //GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/vrviz/prefabs/panel_.prefab", typeof(GameObject));
            GameObject prefab = Resources.Load<GameObject>("Assets/vrviz/prefabs/panel_.prefab");
            Transform panel = null;
            float x = 0;
            float y = 0;
            float z = 0;
            for (int i = 0; i < json.data.Length; i += 12800) {
                x = (float)json.data[i].data;
                y = (float)json.data[i+1].data;
                z = (float)json.data[i+2].data;
                panel = FindChild(tf, child_prefix+i/4, prefab);
                panel.localPosition = new Vector3(x,y,z);
                return; 
            }
        }

        public static void SetPosition(geometry_msgs::Point json, Transform tf) {
            tf.localPosition = new Vector3((float)json.x.data, (float)0, (float)json.y.data);
        }
        public static void SetPosition(geometry_msgs::Vector3 json, Transform tf) {
            tf.localPosition = new Vector3((float)json.x.data, (float)0, (float)json.y.data);
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
            Debug.Log("Set Image Called");
//            byte[] col = ImageUtils.ColourSwap(json);
//            Texture2D tex = new Texture2D((int)json.width.data, (int)json.height.data, TextureFormat.RGB24, false);
//            tex.LoadRawTextureData(col);
//            if(rgb_panel.texture==null){
//                tex.Apply();
//                rgb_panel.texture = tex;
//            }
//            else{
//                Graphics.CopyTexture(tex, rgb_panel.texture);
//            }
//            if(MessageApplications.camera_tex==null){
//                MessageApplications.camera_tex = new Texture2D((int)json.width.data, (int)json.height.data, TextureFormat.RGB24, false);
//            }
//            MessageApplications.camera_tex.LoadRawTextureData(col);
//            if(rgb_panel.texture==null){
//                MessageApplications.camera_tex.Apply();
//                rgb_panel.texture=MessageApplications.camera_tex;
//            }
//            else{
//                Graphics.CopyTexture(MessageApplications.camera_tex, rgb_panel.texture);
//}
//            MessageApplications.camera_tex.Apply();
//            //rgb_panel.texture.LoadRawTextureData(col);
//            Texture2D t = new Texture2D(1,1);
//            t.LoadImage(std_msgs.UInt8.FromArray(json.data));
//            t.Apply();
//            if(rgb_panel == null){
//                Debug.Log("Houston we have a problem.....");
//            }
//            rgb_panel.texture = MessageApplications.camera_tex;
        }

//        //static Texture2D camera_tex = null;
//        public static void SetImageC(sensor_msgs::CompressedImage json, RawImage rgb_panel) {
//            byte[] col = ImageUtils.ColourSwap(json);
//            Texture2D tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
//            tex.LoadRawTextureData(col);
//            //tex.Apply();
//            rgb_panel.texture = tex;
//        }

//        public static void SetImageMaterial(sensor_msgs::Image json, Renderer renderer){
//            Material m = renderer.material;
//            byte[] col = ImageUtils.ColourSwap(json);
//            Texture2D tex = new Texture2D(640, 480, TextureFormat.RGB24, false);
//            tex.LoadRawTextureData(col);
//            m.mainTexture = tex;
//        }

	}
}