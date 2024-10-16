using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(Int8Converter))]
	public class Int8{
		public sbyte data;
	public static string ToRosString() { return "std_msgs.msg:Int8"; }
	}
}