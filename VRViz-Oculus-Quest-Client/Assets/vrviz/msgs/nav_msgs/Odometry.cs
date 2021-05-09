using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class Odometry {
		public Header header;
		public std_msgs.String child_frame_id;
		public PoseWithCovariance pose;
		public TwistWithCovariance twist;
	}
}
