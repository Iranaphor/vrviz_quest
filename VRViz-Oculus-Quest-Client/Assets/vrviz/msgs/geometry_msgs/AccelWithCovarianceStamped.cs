using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class AccelWithCovarianceStamped {
		public std_msgs::Header header;
		public geometry_msgs::AccelWithCovariance accel;
	}
}
