using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class CompressedImage {
		public std_msgs::Header header;
		public std_msgs::String format;
		public std_msgs::UInt8[] data;
	}
}
