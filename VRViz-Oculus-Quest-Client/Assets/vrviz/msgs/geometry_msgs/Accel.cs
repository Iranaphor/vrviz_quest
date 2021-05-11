using System;
using geometry_msgs = vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Accel {
		public geometry_msgs::Vector3 linear;
		public geometry_msgs::Vector3 angular;
	}
}
