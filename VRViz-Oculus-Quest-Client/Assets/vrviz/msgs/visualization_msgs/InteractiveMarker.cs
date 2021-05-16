using Newtonsoft.Json;
using visualization_msgs = VRViz.Messages.visualization_msgs;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

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
		public static string ToRosString() { return "visualization_msgs.msg:InteractiveMarker"; }
	}
}