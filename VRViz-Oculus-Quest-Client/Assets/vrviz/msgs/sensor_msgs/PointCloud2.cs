using System;
using vrviz.msg.std_msgs;
using vrviz.msg.sensor_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class PointCloud2 {
		public Header header;
		public std_msgs.UInt32 height;
		public std_msgs.UInt32 width;
		public PointField[] fields;
		public std_msgs.Bool is_bigendian;
		public std_msgs.UInt32 point_step;
		public std_msgs.UInt32 row_step;
		public std_msgs.UInt8[] data;
		public std_msgs.Bool is_dense;
	}
}
