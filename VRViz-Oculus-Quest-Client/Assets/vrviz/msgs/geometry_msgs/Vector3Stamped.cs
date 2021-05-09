using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Vector3Stamped {
		public Header header;
		public Vector3 vector;
	}
}
