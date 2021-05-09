using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Image {
		public Header header;
		public std_msgs.UInt32 height;
		public std_msgs.UInt32 width;
		public std_msgs.String encoding;
		public std_msgs.UInt8 is_bigendian;
		public std_msgs.UInt32 step;
		public std_msgs.UInt8[] data;
	}
}
