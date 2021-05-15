using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Messages.visualization_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class InteractiveMarker {
		public std_msgs::Header header;
		public geometry_msgs::Pose pose;
		public std_msgs::String name;
		public std_msgs::String description;
		public std_msgs::Float32 scale;
		public visualization_msgs::MenuEntry[] menu_entries;
		public visualization_msgs::InteractiveMarkerControl[] controls;
	}
}