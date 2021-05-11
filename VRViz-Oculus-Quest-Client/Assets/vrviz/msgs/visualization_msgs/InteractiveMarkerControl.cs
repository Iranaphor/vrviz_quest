using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;
using visualization_msgs = vrviz.msg.visualization_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
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
