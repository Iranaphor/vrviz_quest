using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Int64MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::Int64[] data;
	}
}
