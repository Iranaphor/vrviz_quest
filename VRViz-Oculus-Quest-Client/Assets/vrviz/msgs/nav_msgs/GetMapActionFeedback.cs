using Newtonsoft.Json;
using VRViz.Messages.nav_msgs;
using VRViz.Serialiser;
using System;
using VRViz.Messages.actionlib_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class GetMapActionFeedback {
		public std_msgs::Header header;
		public actionlib_msgs::GoalStatus status;
		public nav_msgs::GetMapFeedback feedback;
	}
}