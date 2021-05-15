using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class Odometry {
		public std_msgs::Header header;
		public std_msgs::string child_frame_id;
		public geometry_msgs::PoseWithCovariance pose;
		public geometry_msgs::TwistWithCovariance twist;
	}
}