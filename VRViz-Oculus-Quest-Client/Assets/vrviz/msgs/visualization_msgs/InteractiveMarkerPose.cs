using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class InteractiveMarkerPose {
		public std_msgs::Header header;
		public geometry_msgs::Pose pose;
		public std_msgs::String name;
	}
}