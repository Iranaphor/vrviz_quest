using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class MultiArrayLayout {
		public std_msgs::MultiArrayDimension[] dim;
		public std_msgs::UInt32 data_offset;
	}
}
