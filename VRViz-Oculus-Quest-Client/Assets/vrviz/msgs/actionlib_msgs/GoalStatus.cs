using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.actionlib_msgs {
	public class GoalStatus {
		public actionlib_msgs::GoalID goal_id;
		public std_msgs::uint8 status;
		public std_msgs::string text;
	}
}