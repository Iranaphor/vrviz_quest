using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.nav_msgs {
	public class Path {
		public std_msgs::Header header;
		public geometry_msgs::PoseStamped[] poses;
		public static string ToRosString() { return "nav_msgs.msg:Path"; }
	}
}