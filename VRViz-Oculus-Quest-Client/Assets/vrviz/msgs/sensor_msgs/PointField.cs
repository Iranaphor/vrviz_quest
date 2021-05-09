using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class PointField {
		public std_msgs.String name;
		public std_msgs.UInt32 offset;
		public std_msgs.UInt8 datatype;
		public std_msgs.UInt32 count;
	}
}
