using System;
using vrviz.msg.nav_msgs;
using vrviz.msg.std_msgs;
using vrviz.msg.actionlib_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GetMapActionResult {
		public Header header;
		public GoalStatus status;
		public GetMapResult result;
	}
}
