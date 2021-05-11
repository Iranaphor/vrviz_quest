using System;
using nav_msgs = vrviz.msg.nav_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class OccupancyGrid {
		public std_msgs::Header header;
		public nav_msgs::MapMetaData info;
		public std_msgs::Int8[] data;
	}
}
