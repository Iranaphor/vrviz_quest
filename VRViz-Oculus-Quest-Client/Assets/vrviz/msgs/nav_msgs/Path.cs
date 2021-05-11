using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class Path {
		public std_msgs::Header header;
		public geometry_msgs::PoseStamped[] poses;
	}
}
