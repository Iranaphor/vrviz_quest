using System;
using VRViz.Messages.visualization_msgs;
using VRViz;
using UnityEngine;

namespace VRViz {
    public class MarkerModifiers {
        public static GameObject makeArrow(Color32 colour){
            return new GameObject();
        }

        public static GameObject MakeSphere(Marker mark){
            GameObject obj =  GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (obj.GetComponent<Transform>() == null){
                obj.AddComponent<Transform>(); 
            }
            Transform transform = obj.GetComponent<Transform>();
            transform.localPosition = MarkerUtilities.GetVector(mark.pose.position);
            transform.localScale = MarkerUtilities.GetVector(mark.scale);
            transform.localRotation = MarkerUtilities.GetQuaternion(mark.pose.orientation);
            return obj;
        }
    }

    public class MarkerUtilities{
        public static UnityEngine.Vector3 GetVector(VRViz.Messages.geometry_msgs.Vector3 vector) {         
            return new UnityEngine.Vector3(Convert.ToSingle(vector.x.data), Convert.ToSingle(vector.y.data), Convert.ToSingle(vector.z.data));
        } 
        public static UnityEngine.Vector3 GetVector(VRViz.Messages.geometry_msgs.Point vector) {         
            return new UnityEngine.Vector3(Convert.ToSingle(vector.x.data), Convert.ToSingle(vector.y.data), Convert.ToSingle(vector.z.data));
        } 
        public static UnityEngine.Quaternion GetQuaternion(VRViz.Messages.geometry_msgs.Quaternion quarternion) {         
            return new UnityEngine.Quaternion(Convert.ToSingle(quarternion.x.data), Convert.ToSingle(quarternion.y.data), Convert.ToSingle(quarternion.z.data), Convert.ToSingle(quarternion.w.data));
        }
        public static UnityEngine.Color GetColour(VRViz.Messages.std_msgs.ColorRGBA colour) {
            return new UnityEngine.Color(colour.r.data, colour.g.data, colour.b.data, colour.a.data); //Argument 1: cannot convert from 'VRViz.Messages.std_msgs.Float32' to 'byte'
        }
    }
}

