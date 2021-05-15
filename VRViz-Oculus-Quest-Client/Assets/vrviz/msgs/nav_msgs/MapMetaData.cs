using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class MapMetaData {
		public std_msgs::Time map_load_time;
		public std_msgs::Float32 resolution;
		public std_msgs::UInt32 width;
		public std_msgs::UInt32 height;
		public geometry_msgs::Pose origin;
	}
}