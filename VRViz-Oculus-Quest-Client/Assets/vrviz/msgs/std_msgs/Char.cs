using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(CharConverter))]
	public class Char{
		public char data;
	public static string ToRosString() { return "std_msgs.msg:Char"; }
	}
}