using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(BoolConverter))]
	public class Bool{
		public bool data;
	public static string ToRosString() { return "std_msgs.msg:Bool"; }
	}
}