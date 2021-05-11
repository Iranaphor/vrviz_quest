using System;
using geometry_msgs = vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Polygon {
		public geometry_msgs::Point32[] points;
	}
}
