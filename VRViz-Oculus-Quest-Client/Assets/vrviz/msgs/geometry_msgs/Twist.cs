using System;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Twist {
		public Vector3 linear;
		public Vector3 angular;
	}
}
