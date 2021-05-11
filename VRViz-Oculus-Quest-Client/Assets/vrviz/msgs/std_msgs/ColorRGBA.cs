using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.std_msgs {
	[Serializable]
	public class ColorRGBA {
		public std_msgs::Float32 r;
		public std_msgs::Float32 g;
		public std_msgs::Float32 b;
		public std_msgs::Float32 a;
	}
}
