using System;
using geometry_msgs = vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Pose {
		public geometry_msgs::Point position;
		public geometry_msgs::Quaternion orientation;
	}
}
