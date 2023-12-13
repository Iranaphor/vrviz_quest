using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(Int16Converter))]
	public class Int16{
		public short data;
	public static string ToRosString() { return "std_msgs.msg:Int16"; }
	}
}