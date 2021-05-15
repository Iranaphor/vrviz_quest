using Newtonsoft.Json;
using VRViz.Messages.nav_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.nav_msgs {

	public class GetMapAction {
		public nav_msgs::GetMapActionGoal action_goal;
		public nav_msgs::GetMapActionResult action_result;
		public nav_msgs::GetMapActionFeedback action_feedback;

	}
}