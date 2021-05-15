using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class AccelWithCovariance {
		public geometry_msgs::Accel accel;
		public std_msgs::float64[36] covariance;
	}
}