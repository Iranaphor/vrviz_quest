using Newtonsoft.Json;
using visualization_msgs = VRViz.Messages.visualization_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class MarkerArray {
		public visualization_msgs::Marker[] markers;
		public static string ToRosString() { return "visualization_msgs.msg:MarkerArray"; }
	}
}