using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class Odometry {
		public std_msgs::Header header;
		public std_msgs::String child_frame_id;
		public geometry_msgs::PoseWithCovariance pose;
		public geometry_msgs::TwistWithCovariance twist;
		public static string ToRosString() { return "nav_msgs.msg:Odometry"; }
	}
}