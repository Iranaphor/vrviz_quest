using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class InteractiveMarkerPose {
		public std_msgs::Header header;
		public geometry_msgs::Pose pose;
		public std_msgs::String name;
		public static string ToRosString() { return "visualization_msgs.msg:InteractiveMarkerPose"; }
	}
}