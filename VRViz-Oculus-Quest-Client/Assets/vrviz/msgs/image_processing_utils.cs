using System;
using VRViz.Messages.std_msgs;
using VRViz.Messages.sensor_msgs;
using UnityEngine;

namespace VRViz.Utilities{
    public class ImageUtils{
        public static byte[] ColourSwap(Image input){
            Debug.Log(input.height.data);
            byte[] ret = UInt8.FromArray(input.data);
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