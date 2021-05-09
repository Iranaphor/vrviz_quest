using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class ChannelFloat32 {
		public std_msgs.String name;
		public std_msgs.Float32[] values;
	}
}
