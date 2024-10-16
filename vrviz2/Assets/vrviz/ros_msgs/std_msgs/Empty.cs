using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class Empty {
		public static string ToRosString() { return "std_msgs.msg:Empty"; }
	}
}