using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.actionlib_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.actionlib_msgs {
	public class GoalStatus {
		public actionlib_msgs::GoalID goal_id;
		public std_msgs::UInt8 status;
		public std_msgs::String text;
	}
}