using System;
using VRViz.Messages.std_msgs;
using VRViz.Messages.sensor_msgs;
using UnityEngine;

namespace VRViz.Utilities{
    public class ImageUtils{
        public static byte[] ColourSwap(Image input){
            byte[] ret = UInt8.FromArray(input.data);

            /*
NullReferenceException: Object reference not set to an instance of an object
VRViz.Utilities.ImageUtils.ColourSwap (VRViz.Messages.sensor_msgs.Image input) (at Assets/vrviz/msgs/image_processing_utils.cs:9)
VRViz.Modifiers.ApplyMessage.SetImage (VRViz.Messages.sensor_msgs.Image json, UnityEngine.UI.RawImage rgb_panel) (at Assets/vrviz/modifiers/Transformers.cs:46)
VRViz.Containers.Image.ApplyMessage () (at Assets/vrviz/containers/2d_displays.cs:30)
VRViz.Containers.Base.ApplyIfMessage () (at Assets/vrviz/containers/containers.cs:129)
VRViz.Pipeline.Pipeline.Update () (at Assets/vrviz/Pipeline.cs:63)
            */



            for(int i=0;i<ret.Length;i+=3){
                byte temp = ret[i];
                ret[i] = ret[i+2];
                ret[i+2] = temp;
            }
            return ret;
        }

        public static byte[] ColourSwap(CompressedImage input){
            byte[] ret = UInt8.FromArray(input.data);
            for(int i=0;i<ret.Length;i+=3){
                byte temp = ret[i];
                ret[i] = ret[i+2];
                ret[i+2] = temp;
            }
            return ret;
        }

        public static byte[] ColourSwap(byte[] input){
            for(int i=0;i<input.Length;i+=3){
                byte temp = input[i];
                input[i] = input[i+2];
                input[i+2] = temp;
            }
            return input;
        }   

        public static Color32[] GetColor(Image input){
            byte[] bits = UInt8.FromArray(input.data);
            Color32[] ret = new Color32[bits.Length/3];
            for(int i=0;i<ret.Length;i+=3){
                ret[i/3] = new Color32(bits[i], bits[i+1],bits[i+2], 255);
            }
            return ret;
        }
    }


}