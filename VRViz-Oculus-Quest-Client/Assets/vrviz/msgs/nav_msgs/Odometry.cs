using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class Odometry {
		public std_msgs::Header header;
		public std_msgs::String child_frame_id;
		public geometry_msgs::PoseWithCovariance pose;
		public geometry_msgs::TwistWithCovariance twist;
	}
}
