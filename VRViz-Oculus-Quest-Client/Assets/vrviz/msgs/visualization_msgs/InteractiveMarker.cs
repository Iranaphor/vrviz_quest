using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;
using visualization_msgs = vrviz.msg.visualization_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
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
