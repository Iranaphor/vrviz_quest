using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class JointState {
		public std_msgs::Header header;
		public std_msgs::String[] name;
		public std_msgs::Float64[] position;
		public std_msgs::Float64[] velocity;
		public std_msgs::Float64[] effort;
	}
}
