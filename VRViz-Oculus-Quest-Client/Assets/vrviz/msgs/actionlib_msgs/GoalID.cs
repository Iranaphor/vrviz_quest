using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.actionlib_msgs {
	[Serializable]
	public class GoalID {
		public std_msgs::Time stamp;
		public std_msgs::String id;
	}
}
