using System;
using Newtonsoft.Json;
using VRViz.Serialiser;
// https://stackoverflow.com/questions/43379334/is-there-a-1-value-class-wrapper-type-in-net
namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(StringConverter))]
	public class String{
		public string data;
		public static string ToRosString() { return "std_msgs.msg:String"; }
		// public static implicit operator string(String s){ return data.Value; }
	}
}