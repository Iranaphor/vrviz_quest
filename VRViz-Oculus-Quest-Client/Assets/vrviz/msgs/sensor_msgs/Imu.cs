using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Imu {
		public Header header;
		public Quaternion orientation;
		public std_msgs.Float64[] orientation_covariance;
		public Vector3 angular_velocity;
		public std_msgs.Float64[] angular_velocity_covariance;
		public Vector3 linear_acceleration;
		public std_msgs.Float64[] linear_acceleration_covariance;
	}
}
