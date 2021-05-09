using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class MultiArrayDimension {
		public std_msgs.String label;
		public std_msgs.UInt32 size;
		public std_msgs.UInt32 stride;
	}
}
