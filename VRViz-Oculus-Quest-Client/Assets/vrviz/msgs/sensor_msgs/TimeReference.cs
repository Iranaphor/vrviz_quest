using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class TimeReference {
		public Header header;
		public std_msgs.Time Time_ref;
		public std_msgs.String source;
	}
}
