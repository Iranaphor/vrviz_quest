using VRViz.Messages.std_msgs;
using VRViz.Messages.float32;
using System;
using Newtonsoft.Json;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.nav_msgs {

	public class GridCells {
		public std_msgs::Header header;
		public std_msgs::float32 cell_width;
		public std_msgs::float32 cell_height;
		public geometry_msgs::Point[] cells;

	}
}