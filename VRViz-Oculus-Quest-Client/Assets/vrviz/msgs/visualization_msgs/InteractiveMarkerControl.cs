using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class InteractiveMarkerControl {
		public std_msgs::string name;
		public geometry_msgs::Quaternion orientation;
		public std_msgs::uint8 orientation_mode;
		public std_msgs::uint8 interaction_mode;
		public std_msgs::bool always_visible;
		public visualization_msgs::Marker[] markers;
		public std_msgs::bool independent_marker_orientation;
		public std_msgs::string description;
	}
}