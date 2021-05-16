using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	public class Time{
		public ulong secs;
		public ulong nsecs;
		public static string ToRosString() { return "std_msgs.msg:Time"; }
	}
}
