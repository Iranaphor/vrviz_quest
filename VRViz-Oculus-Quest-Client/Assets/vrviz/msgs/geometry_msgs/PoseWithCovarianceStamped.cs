using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class PoseWithCovarianceStamped {
		public Header header;
		public PoseWithCovariance pose;
	}
}
