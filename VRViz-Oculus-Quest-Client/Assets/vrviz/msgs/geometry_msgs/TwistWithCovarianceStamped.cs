using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class TwistWithCovarianceStamped {
		public std_msgs::Header header;
		public geometry_msgs::TwistWithCovariance twist;
		public static string ToRosString() { return "geometry_msgs.msg:TwistWithCovarianceStamped"; }
	}
}