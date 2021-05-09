using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarkerFeedback {
		public Header header;
		public std_msgs.String client_id;
		public std_msgs.String marker_name;
		public std_msgs.String control_name;
		public std_msgs.UInt8 event_type;
		public Pose pose;
		public std_msgs.UInt32 menu_entry_id;
		public Point mouse_point;
		public std_msgs.Bool mouse_point_valid;
	}
}
