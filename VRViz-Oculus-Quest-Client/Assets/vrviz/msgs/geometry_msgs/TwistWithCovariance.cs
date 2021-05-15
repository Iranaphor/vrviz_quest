using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.float64[36];

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.geometry_msgs {

	public class TwistWithCovariance {
		public geometry_msgs::Twist twist;
		public std_msgs::float64[36] covariance;

	}
}