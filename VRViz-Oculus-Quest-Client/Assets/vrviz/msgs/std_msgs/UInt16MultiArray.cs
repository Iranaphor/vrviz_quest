using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class UInt16MultiArray {
		public MultiArrayLayout layout;
		public std_msgs.UInt16[] data;
	}
}
