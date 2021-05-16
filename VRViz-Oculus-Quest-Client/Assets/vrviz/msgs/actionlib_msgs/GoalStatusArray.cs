using Newtonsoft.Json;
using actionlib_msgs = VRViz.Messages.actionlib_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.actionlib_msgs {
	public class GoalStatusArray {
		public std_msgs::Header header;
		public actionlib_msgs::GoalStatus[] status_list;
		public static string ToRosString() { return "actionlib_msgs.msg:GoalStatusArray"; }
	}
}