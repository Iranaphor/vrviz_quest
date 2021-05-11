using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarkerFeedback {
		public std_msgs::Header header;
		public std_msgs::String client_id;
		public std_msgs::String marker_name;
		public std_msgs::String control_name;
		public std_msgs::UInt8 event_type;
		public geometry_msgs::Pose pose;
		public std_msgs::UInt32 menu_entry_id;
		public geometry_msgs::Point mouse_point;
		public std_msgs::Bool mouse_point_valid;
	}
}
