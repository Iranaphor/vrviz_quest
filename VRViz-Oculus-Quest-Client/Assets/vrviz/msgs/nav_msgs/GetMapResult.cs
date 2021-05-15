using Newtonsoft.Json;
using VRViz.Messages.nav_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class GetMapResult {
		public nav_msgs::OccupancyGrid map;
	}
}