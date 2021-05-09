using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class MultiArrayLayout {
		public MultiArrayDimension[] dim;
		public std_msgs.UInt32 data_offset;
	}
}
