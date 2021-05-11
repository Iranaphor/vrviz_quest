using System;
using nav_msgs = vrviz.msg.nav_msgs;
using std_msgs = vrviz.msg.std_msgs;
using actionlib_msgs = vrviz.msg.actionlib_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GetMapActionGoal {
		public std_msgs::Header header;
		public actionlib_msgs::GoalID goal_id;
		public nav_msgs::GetMapGoal goal;
	}
}
