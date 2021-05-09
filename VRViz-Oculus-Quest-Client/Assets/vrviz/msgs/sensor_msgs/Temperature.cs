using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Temperature {
		public Header header;
		public std_msgs.Float64 temperature;
		public std_msgs.Float64 variance;
	}
}
