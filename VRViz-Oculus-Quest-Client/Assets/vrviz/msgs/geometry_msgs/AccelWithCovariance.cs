using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class AccelWithCovariance {
		public geometry_msgs::Accel accel;
		public std_msgs::Float64[] covariance;
	}
}
