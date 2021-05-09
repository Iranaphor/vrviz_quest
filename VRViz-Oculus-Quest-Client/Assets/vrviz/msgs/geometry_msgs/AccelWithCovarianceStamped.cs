using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class AccelWithCovarianceStamped {
		public Header header;
		public AccelWithCovariance accel;
	}
}
