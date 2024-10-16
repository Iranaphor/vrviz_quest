using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(Int32Converter))]
	public class Int32{
		public int data;
	public static string ToRosString() { return "std_msgs.msg:Int32"; }
	}
}