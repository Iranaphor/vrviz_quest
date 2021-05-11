using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class TwistWithCovariance {
		public geometry_msgs::Twist twist;
		public std_msgs::Float64[] covariance;
	}
}
