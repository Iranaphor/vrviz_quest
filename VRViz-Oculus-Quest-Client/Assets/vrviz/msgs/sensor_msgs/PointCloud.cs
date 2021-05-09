using System;
using vrviz.msg.sensor_msgs;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class PointCloud {
		public Header header;
		public Point32[] points;
		public ChannelFloat32[] channels;
	}
}
