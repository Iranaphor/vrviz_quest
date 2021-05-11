using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class RegionOfInterest {
		public std_msgs::UInt32 x_offset;
		public std_msgs::UInt32 y_offset;
		public std_msgs::UInt32 height;
		public std_msgs::UInt32 width;
		public std_msgs::Bool do_rectify;
	}
}
