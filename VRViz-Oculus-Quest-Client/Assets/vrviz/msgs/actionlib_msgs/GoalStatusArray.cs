using System;
using std_msgs = vrviz.msg.std_msgs;
using actionlib_msgs = vrviz.msg.actionlib_msgs;

namespace vrviz.msg.actionlib_msgs {
	[Serializable]
	public class GoalStatusArray {
		public std_msgs::Header header;
		public actionlib_msgs::GoalStatus[] status_list;
	}
}
