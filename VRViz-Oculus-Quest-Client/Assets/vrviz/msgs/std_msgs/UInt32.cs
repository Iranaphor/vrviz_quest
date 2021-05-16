using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(UInt32Converter))]
	public class UInt32{
		public uint data;
	public static string ToRosString() { return "std_msgs.msg:UInt32"; }
	}
}