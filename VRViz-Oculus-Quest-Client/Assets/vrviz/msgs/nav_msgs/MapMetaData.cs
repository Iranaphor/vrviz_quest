using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class MapMetaData {
		public std_msgs.Time map_load_Time;
		public std_msgs.Float32 resolution;
		public std_msgs.UInt32 width;
		public std_msgs.UInt32 height;
		public Pose origin;
	}
}
