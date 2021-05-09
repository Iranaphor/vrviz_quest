using System;
using vrviz.msg.geometry_msgs;
using vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class TwistWithCovariance {
		public Twist twist;
		public std_msgs.Float64[] covariance;
	}
}
