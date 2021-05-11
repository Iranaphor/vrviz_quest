using System;
using actionlib_msgs = vrviz.msg.actionlib_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.actionlib_msgs {
	[Serializable]
	public class GoalStatus {
		public actionlib_msgs::GoalID goal_id;
		public std_msgs::UInt8 status;
		public std_msgs::String text;
	}
}
