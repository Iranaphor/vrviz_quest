using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(DurationConverter))]
	public class Duration{
		public ulong data;
	public static string ToRosString() { return "std_msgs.msg:Duration"; }
	}
}