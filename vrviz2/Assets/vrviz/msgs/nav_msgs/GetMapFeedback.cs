using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class GetMapFeedback {
		public static string ToRosString() { return "nav_msgs.msg:GetMapFeedback"; }
	}
}