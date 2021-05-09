using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class UInt8MultiArray {
		public MultiArrayLayout layout;
		public std_msgs.UInt8[] data;
	}
}
