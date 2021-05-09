using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarkerPose {
		public Header header;
		public Pose pose;
		public std_msgs.String name;
	}
}
