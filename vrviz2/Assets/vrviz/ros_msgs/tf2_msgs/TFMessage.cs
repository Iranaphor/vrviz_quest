using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.tf2_msgs {
	public class TFMessage {
		public std_msgs::Header header;
		public geometry_msgs::TransformStamped[] transforms;
		public static string ToRosString() { return "tf2_msgs.msg:TFMessage"; }
	}
}