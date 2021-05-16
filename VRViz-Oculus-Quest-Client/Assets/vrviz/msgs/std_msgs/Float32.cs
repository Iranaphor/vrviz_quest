using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(Float32Converter))]
	public class Float32{
		public float data;
	public static string ToRosString() { return "std_msgs.msg:Float32"; }
	}
}