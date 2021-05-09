using System;
using vrviz.msg.nav_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GetMapResult {
		public OccupancyGrid map;
	}
}
