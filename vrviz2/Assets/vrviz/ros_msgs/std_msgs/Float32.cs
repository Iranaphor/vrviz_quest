using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(Float32Converter))]
	public class Float32{
		public float data;
		public static string ToRosString() { return "std_msgs.msg:Float32"; }
		public static float[] FromArray(Float32[] inArr){
			float[] ret = new float[inArr.Length];
			for(int i=0;i<inArr.Length;i++){
				ret[i]=inArr[i].data;
			}
			return ret;
		}
	}
}