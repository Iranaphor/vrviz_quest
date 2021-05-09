using System;
using vrviz.msg.std_msgs;
using vrviz.msg.sensor_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class NavSatFix {
		public Header header;
		public NavSatStatus status;
		public std_msgs.Float64 latitude;
		public std_msgs.Float64 longitude;
		public std_msgs.Float64 altitude;
		public std_msgs.Float64[] position_covariance;
		public std_msgs.UInt8 position_covariance_type;
	}
}
