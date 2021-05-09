using System;
using vrviz.msg.std_msgs;
using vrviz.msg.actionlib_msgs;

namespace vrviz.msg.actionlib_msgs {
	[Serializable]
	public class GoalStatusArray {
		public Header header;
		public GoalStatus[] status_list;
	}
}
