using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(Int64Converter))]
	public class Int64{
		public long data;
	public static string ToRosString() { return "std_msgs.msg:Int64"; }
	}
}