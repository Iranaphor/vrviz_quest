using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(ByteConverter))]
	public class Byte{
		public byte data;
	public static string ToRosString() { return "std_msgs.msg:Byte"; }
	}
}