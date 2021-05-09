using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class TwistStamped {
		public Header header;
		public Twist twist;
	}
}
