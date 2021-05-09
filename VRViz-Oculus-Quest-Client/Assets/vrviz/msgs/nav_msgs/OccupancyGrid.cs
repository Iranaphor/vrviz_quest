using System;
using vrviz.msg.nav_msgs;
using vrviz.msg.std_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class OccupancyGrid {
		public Header header;
		public MapMetaData info;
		public std_msgs.Int8[] data;
	}
}
