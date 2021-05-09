using System;
using vrviz.msg.std_msgs;
using vrviz.msg.actionlib_msgs;

namespace vrviz.msg.actionlib_msgs {
	[Serializable]
	public class GoalStatus {
		public GoalID goal_id;
		public std_msgs.UInt8 status;
		public std_msgs.String text;
	}
}
