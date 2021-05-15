using VRViz.Messages.std_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.actionlib_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.actionlib_msgs {

	public class GoalStatusArray {
		public std_msgs::Header header;
		public actionlib_msgs::GoalStatus[] status_list;

	}
}