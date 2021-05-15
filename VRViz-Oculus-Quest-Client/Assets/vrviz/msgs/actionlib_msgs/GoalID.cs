using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.actionlib_msgs {
	public class GoalID {
		public std_msgs::time stamp;
		public std_msgs::string id;
	}
}