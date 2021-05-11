using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class UInt8MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::UInt8[] data;
	}
}
