using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class QuaternionStamped {
		public Header header;
		public Quaternion quaternion;
	}
}
