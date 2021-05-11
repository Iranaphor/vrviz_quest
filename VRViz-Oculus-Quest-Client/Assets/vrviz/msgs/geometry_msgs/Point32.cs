using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Point32 {
		public std_msgs::Float32 x;
		public std_msgs::Float32 y;
		public std_msgs::Float32 z;
	}
}
