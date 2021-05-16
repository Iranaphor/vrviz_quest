using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using nav_msgs = VRViz.Messages.nav_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class GetMapResult {
		public nav_msgs::OccupancyGrid map;
		public static string ToRosString() { return "nav_msgs.msg:GetMapResult"; }
	}
}