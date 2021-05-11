using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class ByteMultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::Byte[] data;
	}
}
