using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class RelativeHumidity {
		public Header header;
		public std_msgs.Float64 relative_humidity;
		public std_msgs.Float64 variance;
	}
}
