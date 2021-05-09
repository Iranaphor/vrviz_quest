using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Joy {
		public Header header;
		public std_msgs.Float32[] axes;
		public std_msgs.Int32[] buttons;
	}
}
