using System;
using vrviz.msg.visualization_msgs;
using vrviz.msg.std_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarkerInit {
		public std_msgs.String server_id;
		public std_msgs.UInt64 seq_num;
		public InteractiveMarker[] markers;
	}
}
