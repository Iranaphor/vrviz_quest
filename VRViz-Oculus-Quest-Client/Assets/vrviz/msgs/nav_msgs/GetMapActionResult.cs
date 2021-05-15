using Newtonsoft.Json;
using System;
using VRViz.Messages.nav_msgs;
using VRViz.Serialiser;
using VRViz.Messages.std_msgs;
using VRViz.Messages.actionlib_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.nav_msgs {

	public class GetMapActionResult {
		public std_msgs::Header header;
		public actionlib_msgs::GoalStatus status;
		public nav_msgs::GetMapResult result;

	}
}