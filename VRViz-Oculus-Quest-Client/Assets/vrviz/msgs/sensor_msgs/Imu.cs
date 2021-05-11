using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Imu {
		public std_msgs::Header header;
		public geometry_msgs::Quaternion orientation;
		public std_msgs::Float64[] orientation_covariance;
		public geometry_msgs::Vector3 angular_velocity;
		public std_msgs::Float64[] angular_velocity_covariance;
		public geometry_msgs::Vector3 linear_acceleration;
		public std_msgs::Float64[] linear_acceleration_covariance;
	}
}
