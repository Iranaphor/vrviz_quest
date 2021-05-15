using VRViz.Messages.std_msgs;
using VRViz.Messages.string;
using System;
using Newtonsoft.Json;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.visualization_msgs {

	public class InteractiveMarkerPose {
		public std_msgs::Header header;
		public geometry_msgs::Pose pose;
		public std_msgs::string name;

	}
}