using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class PoseWithCovariance {
		public geometry_msgs::Pose pose;
		public std_msgs::Float64[] covariance;
	}
}
