using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.nav_msgs {
	[Serializable]
	public class GridCells {
		public Header header;
		public std_msgs.Float32 cell_width;
		public std_msgs.Float32 cell_height;
		public Point[] cells;
	}
}
