using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Int16MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::Int16[] data;
	}
}
