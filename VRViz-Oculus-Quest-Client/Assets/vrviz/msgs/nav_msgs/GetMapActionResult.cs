using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class GetMapActionResult {
		public std_msgs::Header header;
		public actionlib_msgs::GoalStatus status;
		public nav_msgs::GetMapResult result;
	}
}