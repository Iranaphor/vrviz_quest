using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.sensor_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
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