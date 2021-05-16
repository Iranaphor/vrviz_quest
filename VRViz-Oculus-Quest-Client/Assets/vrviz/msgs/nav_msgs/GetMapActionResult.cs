using Newtonsoft.Json;
using actionlib_msgs = VRViz.Messages.actionlib_msgs;
using VRViz.Serialiser;
using System;
using nav_msgs = VRViz.Messages.nav_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class GetMapActionResult {
		public std_msgs::Header header;
		public actionlib_msgs::GoalStatus status;
		public nav_msgs::GetMapResult result;
		public static string ToRosString() { return "nav_msgs.msg:GetMapActionResult"; }
	}
}