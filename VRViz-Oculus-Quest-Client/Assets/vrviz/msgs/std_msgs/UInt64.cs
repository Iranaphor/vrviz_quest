using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(UInt64Converter))]
	public class UInt64{
		public ulong data;
	public static string ToRosString() { return "std_msgs.msg:UInt64"; }
	}
}