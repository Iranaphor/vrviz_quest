using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Float64MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::Float64[] data;
	}
}
