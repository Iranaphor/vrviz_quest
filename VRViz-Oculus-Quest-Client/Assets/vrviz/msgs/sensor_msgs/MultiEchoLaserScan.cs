using System;
using vrviz.msg.std_msgs;
using vrviz.msg.sensor_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class MultiEchoLaserScan {
		public Header header;
		public std_msgs.Float32 angle_min;
		public std_msgs.Float32 angle_max;
		public std_msgs.Float32 angle_increment;
		public std_msgs.Float32 time_increment;
		public std_msgs.Float32 scan_time;
		public std_msgs.Float32 range_min;
		public std_msgs.Float32 range_max;
		public LaserEcho[] ranges;
		public LaserEcho[] intensities;
	}
}
