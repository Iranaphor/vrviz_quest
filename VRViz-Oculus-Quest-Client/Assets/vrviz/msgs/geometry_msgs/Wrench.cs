using System;
using geometry_msgs = vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Wrench {
		public geometry_msgs::Vector3 force;
		public geometry_msgs::Vector3 torque;
	}
}
