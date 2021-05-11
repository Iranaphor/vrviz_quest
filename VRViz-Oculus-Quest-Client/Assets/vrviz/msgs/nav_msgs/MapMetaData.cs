using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class MapMetaData {
		public std_msgs::Time map_load_Time;
		public std_msgs::Float32 resolution;
		public std_msgs::UInt32 width;
		public std_msgs::UInt32 height;
		public geometry_msgs::Pose origin;
	}
}
