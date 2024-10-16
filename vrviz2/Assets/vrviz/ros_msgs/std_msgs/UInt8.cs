using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(UInt8Converter))]
	public class UInt8{
		public byte data;
		public static string ToRosString() { return "std_msgs.msg:UInt8"; }
		public static byte[] FromArray(UInt8[] inArr){
			byte[] ret = new byte[inArr.Length];
			for(int i=0;i<inArr.Length;i++){
				ret[i]=inArr[i].data;
			}
			return ret;
		}
	}
}