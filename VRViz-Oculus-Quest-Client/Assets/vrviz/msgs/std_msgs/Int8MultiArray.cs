using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Int8MultiArray {
		public MultiArrayLayout layout;
		public std_msgs.Int8[] data;
	}
}
