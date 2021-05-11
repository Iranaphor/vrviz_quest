using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GridCells {
		public std_msgs::Header header;
		public std_msgs::Float32 cell_width;
		public std_msgs::Float32 cell_height;
		public geometry_msgs::Point[] cells;
	}
}
