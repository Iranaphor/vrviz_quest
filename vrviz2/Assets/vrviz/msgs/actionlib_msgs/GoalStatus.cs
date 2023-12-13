using Newtonsoft.Json;
using actionlib_msgs = VRViz.Messages.actionlib_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.actionlib_msgs {
	public class GoalStatus {
		public actionlib_msgs::GoalID goal_id;
		public std_msgs::UInt8 status;
		public std_msgs::String text;
		public static string ToRosString() { return "actionlib_msgs.msg:GoalStatus"; }
	}
}