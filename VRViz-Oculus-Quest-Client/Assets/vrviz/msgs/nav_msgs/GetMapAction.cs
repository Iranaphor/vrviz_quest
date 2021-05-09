using System;
using vrviz.msg.nav_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GetMapAction {
		public GetMapActionGoal action_goal;
		public GetMapActionResult action_result;
		public GetMapActionFeedback action_feedback;
	}
}
