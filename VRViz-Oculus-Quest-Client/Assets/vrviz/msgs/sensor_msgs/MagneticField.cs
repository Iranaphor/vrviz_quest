using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class MagneticField {
		public std_msgs::Header header;
		public geometry_msgs::Vector3 magnetic_field;
		public std_msgs::Float64[] magnetic_field_covariance;
	}
}
