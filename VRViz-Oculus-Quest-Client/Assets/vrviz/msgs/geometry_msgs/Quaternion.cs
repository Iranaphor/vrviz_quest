using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Quaternion {
		public std_msgs::Float64 x;
		public std_msgs::Float64 y;
		public std_msgs::Float64 z;
		public std_msgs::Float64 w;
	}
}
