using System;
using nav_msgs = vrviz.msg.nav_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GetMapResult {
		public nav_msgs::OccupancyGrid map;
	}
}
