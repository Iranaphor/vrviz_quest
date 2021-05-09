using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class Path {
		public Header header;
		public PoseStamped[] poses;
	}
}
