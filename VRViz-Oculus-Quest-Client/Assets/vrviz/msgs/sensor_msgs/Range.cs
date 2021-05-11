using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Range {
		public std_msgs::Header header;
		public std_msgs::UInt8 radiation_type;
		public std_msgs::Float32 field_of_view;
		public std_msgs::Float32 min_range;
		public std_msgs::Float32 max_range;
		public std_msgs::Float32 range;
	}
}
