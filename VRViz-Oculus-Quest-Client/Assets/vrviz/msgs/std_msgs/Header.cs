using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class Header {
		public std_msgs::UInt32 seq;
		public std_msgs::Time stamp;
		public std_msgs::String frame_id;
	}
}
