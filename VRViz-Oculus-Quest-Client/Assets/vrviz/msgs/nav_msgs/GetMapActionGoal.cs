using System;
using vrviz.msg.nav_msgs;
using vrviz.msg.std_msgs;
using vrviz.msg.actionlib_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GetMapActionGoal {
		public Header header;
		public GoalID goal_id;
		public GetMapGoal goal;
	}
}
