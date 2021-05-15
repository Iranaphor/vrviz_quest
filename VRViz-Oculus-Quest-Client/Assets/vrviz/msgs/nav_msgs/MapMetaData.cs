using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class MapMetaData {
		public std_msgs::time map_load_time;
		public std_msgs::float32 resolution;
		public std_msgs::uint32 width;
		public std_msgs::uint32 height;
		public geometry_msgs::Pose origin;
	}
}