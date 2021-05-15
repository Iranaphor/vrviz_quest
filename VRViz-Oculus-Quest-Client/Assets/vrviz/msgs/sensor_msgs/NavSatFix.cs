using Newtonsoft.Json;
using VRViz.Messages.sensor_msgs;
using System;
using VRViz.Serialiser;
using VRViz.Messages.float64;
using VRViz.Messages.std_msgs;
using VRViz.Messages.float64[9];
using VRViz.Messages.uint8;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class NavSatFix {
		public std_msgs::Header header;
		public sensor_msgs::NavSatStatus status;
		public std_msgs::float64 latitude;
		public std_msgs::float64 longitude;
		public std_msgs::float64 altitude;
		public std_msgs::float64[9] position_covariance;
		public std_msgs::uint8 position_covariance_type;

	}
}