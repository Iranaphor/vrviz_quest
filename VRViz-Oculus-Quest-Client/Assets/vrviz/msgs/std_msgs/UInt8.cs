using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(UInt8Converter))]
	public class UInt8{
		public byte data;
	public static string ToRosString() { return "std_msgs.msg:UInt8"; }
	}
}