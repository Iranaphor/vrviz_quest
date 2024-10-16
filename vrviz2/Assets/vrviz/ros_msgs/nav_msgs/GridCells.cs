using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class GridCells {
		public std_msgs::Header header;
		public std_msgs::Float32 cell_width;
		public std_msgs::Float32 cell_height;
		public geometry_msgs::Point[] cells;
		public static string ToRosString() { return "nav_msgs.msg:GridCells"; }
	}
}