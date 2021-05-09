using System;
using vrviz.msg.visualization_msgs;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarker {
		public Header header;
		public Pose pose;
		public std_msgs.String name;
		public std_msgs.String description;
		public std_msgs.Float32 scale;
		public MenuEntry[] menu_entries;
		public InteractiveMarkerControl[] controls;
	}
}
