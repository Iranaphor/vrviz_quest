using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;
using sensor_msgs = vrviz.msg.sensor_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class PointCloud {
		public std_msgs::Header header;
		public geometry_msgs::Point32[] points;
		public sensor_msgs::ChannelFloat32[] channels;
	}
}
