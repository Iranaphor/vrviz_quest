using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarkerPose {
		public std_msgs::Header header;
		public geometry_msgs::Pose pose;
		public std_msgs::String name;
	}
}
