using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class UInt64MultiArray {
		public MultiArrayLayout layout;
		public std_msgs.UInt64[] data;
	}
}
