using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.actionlib_msgs {
	public class GoalID {
		public std_msgs::Time stamp;
		public std_msgs::String id;
		public static string ToRosString() { return "actionlib_msgs.msg:GoalID"; }
	}
}