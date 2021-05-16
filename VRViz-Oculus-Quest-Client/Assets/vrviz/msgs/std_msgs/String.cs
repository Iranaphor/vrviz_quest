using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(StringConverter))]
	public class String{
		public string data;
	public static string ToRosString() { return "std_msgs.msg:String"; }
	}
}