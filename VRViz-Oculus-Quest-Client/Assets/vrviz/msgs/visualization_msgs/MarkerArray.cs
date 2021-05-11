using System;
using visualization_msgs = vrviz.msg.visualization_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class MarkerArray {
		public visualization_msgs::Marker[] markers;
	}
}
