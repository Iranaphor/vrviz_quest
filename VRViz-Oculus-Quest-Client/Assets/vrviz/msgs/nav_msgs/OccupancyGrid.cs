using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class OccupancyGrid {
		public std_msgs::Header header;
		public nav_msgs::MapMetaData info;
		public std_msgs::int8[] data;
	}
}