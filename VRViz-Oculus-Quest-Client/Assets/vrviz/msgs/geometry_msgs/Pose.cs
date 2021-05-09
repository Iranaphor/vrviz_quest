using System;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Pose {
		public Point position;
		public Quaternion orientation;
	}
}
