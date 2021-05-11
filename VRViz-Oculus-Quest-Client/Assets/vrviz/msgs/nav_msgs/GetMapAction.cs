using System;
using nav_msgs = vrviz.msg.nav_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GetMapAction {
		public nav_msgs::GetMapActionGoal action_goal;
		public nav_msgs::GetMapActionResult action_result;
		public nav_msgs::GetMapActionFeedback action_feedback;
	}
}
