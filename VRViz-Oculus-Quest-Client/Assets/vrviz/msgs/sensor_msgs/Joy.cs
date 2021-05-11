using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Joy {
		public std_msgs::Header header;
		public std_msgs::Float32[] axes;
		public std_msgs::Int32[] buttons;
	}
}
