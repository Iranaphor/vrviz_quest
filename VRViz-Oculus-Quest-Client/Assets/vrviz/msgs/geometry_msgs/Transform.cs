using System;
using geometry_msgs = vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Transform {
		public geometry_msgs::Vector3 translation;
		public geometry_msgs::Quaternion rotation;
	}
}
