using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class PoseArray {
		public Header header;
		public Pose[] poses;
	}
}
