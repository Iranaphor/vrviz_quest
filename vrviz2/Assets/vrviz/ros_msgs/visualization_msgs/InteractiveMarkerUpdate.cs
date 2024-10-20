using Newtonsoft.Json;
using visualization_msgs = VRViz.Messages.visualization_msgs;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class InteractiveMarkerUpdate {
		public std_msgs::String server_id;
		public std_msgs::UInt64 seq_num;
		public std_msgs::UInt8 type;
		public visualization_msgs::InteractiveMarker[] markers;
		public visualization_msgs::InteractiveMarkerPose[] poses;
		public std_msgs::String[] erases;
		public static string ToRosString() { return "visualization_msgs.msg:InteractiveMarkerUpdate"; }
	}
}