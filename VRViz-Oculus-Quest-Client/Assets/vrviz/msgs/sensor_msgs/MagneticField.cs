using VRViz.Messages.std_msgs;
using System;
using Newtonsoft.Json;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;
using VRViz.Messages.float64[9];

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class MagneticField {
		public std_msgs::Header header;
		public geometry_msgs::Vector3 magnetic_field;
		public std_msgs::float64[9] magnetic_field_covariance;

	}
}