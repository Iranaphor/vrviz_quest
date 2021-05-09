using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Int32MultiArray {
		public MultiArrayLayout layout;
		public std_msgs.Int32[] data;
	}
}
