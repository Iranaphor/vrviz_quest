using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Messages.visualization_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class InteractiveMarkerControl {
		public std_msgs::String name;
		public geometry_msgs::Quaternion orientation;
		public std_msgs::UInt8 orientation_mode;
		public std_msgs::UInt8 interaction_mode;
		public std_msgs::Bool always_visible;
		public visualization_msgs::Marker[] markers;
		public std_msgs::Bool independent_marker_orientation;
		public std_msgs::String description;
	}
}