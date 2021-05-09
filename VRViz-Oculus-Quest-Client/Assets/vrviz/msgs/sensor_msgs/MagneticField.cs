using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class MagneticField {
		public Header header;
		public Vector3 magnetic_field;
		public std_msgs.Float64[] magnetic_field_covariance;
	}
}
