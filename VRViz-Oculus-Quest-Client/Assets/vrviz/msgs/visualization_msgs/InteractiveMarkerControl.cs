using System;
using vrviz.msg.visualization_msgs;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarkerControl {
		public std_msgs.String name;
		public Quaternion orientation;
		public std_msgs.UInt8 orientation_mode;
		public std_msgs.UInt8 interaction_mode;
		public std_msgs.Bool always_visible;
		public Marker[] markers;
		public std_msgs.Bool independent_marker_orientation;
		public std_msgs.String description;
	}
}
