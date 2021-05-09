using System;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Wrench {
		public Vector3 force;
		public Vector3 torque;
	}
}
