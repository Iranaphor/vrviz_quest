using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Float32MultiArray {
		public MultiArrayLayout layout;
		public std_msgs.Float32[] data;
	}
}
