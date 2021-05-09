using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class ByteMultiArray {
		public MultiArrayLayout layout;
		public std_msgs.Byte[] data;
	}
}
