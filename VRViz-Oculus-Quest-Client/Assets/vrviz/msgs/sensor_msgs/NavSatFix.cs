using System;
using std_msgs = vrviz.msg.std_msgs;
using sensor_msgs = vrviz.msg.sensor_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class NavSatFix {
		public std_msgs::Header header;
		public sensor_msgs::NavSatStatus status;
		public std_msgs::Float64 latitude;
		public std_msgs::Float64 longitude;
		public std_msgs::Float64 altitude;
		public std_msgs::Float64[] position_covariance;
		public std_msgs::UInt8 position_covariance_type;
	}
}
