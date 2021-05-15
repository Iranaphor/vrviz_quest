using VRViz.Messages.std_msgs;
using System;
using Newtonsoft.Json;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;
using VRViz.Messages.float64[9];

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class Imu {
		public std_msgs::Header header;
		public geometry_msgs::Quaternion orientation;
		public std_msgs::float64[9] orientation_covariance;
		public geometry_msgs::Vector3 angular_velocity;
		public std_msgs::float64[9] angular_velocity_covariance;
		public geometry_msgs::Vector3 linear_acceleration;
		public std_msgs::float64[9] linear_acceleration_covariance;

	}
}