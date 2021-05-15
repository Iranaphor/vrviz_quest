using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class Path {
		public std_msgs::Header header;
		public geometry_msgs::PoseStamped[] poses;
	}
}