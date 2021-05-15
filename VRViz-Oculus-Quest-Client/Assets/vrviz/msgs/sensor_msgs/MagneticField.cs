using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class MagneticField {
		public std_msgs::Header header;
		public geometry_msgs::Vector3 magnetic_field;
		public std_msgs::Float64[] magnetic_field_covariance;
	}
}