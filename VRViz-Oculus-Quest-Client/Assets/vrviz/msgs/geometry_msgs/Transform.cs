using System;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Transform {
		public Vector3 translation;
		public Quaternion rotation;
	}
}
