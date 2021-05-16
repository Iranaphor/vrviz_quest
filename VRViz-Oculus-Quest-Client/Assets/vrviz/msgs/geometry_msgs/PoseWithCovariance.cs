using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class PoseWithCovariance {
		public geometry_msgs::Pose pose;
		public std_msgs::Float64[] covariance;
		public static string ToRosString() { return "geometry_msgs.msg:PoseWithCovariance"; }
	}
}