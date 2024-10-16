using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class Header {
		public std_msgs::UInt32 seq;
		public std_msgs::Time stamp;
		public std_msgs::String frame_id;
		public static string ToRosString() { return "std_msgs.msg:Header"; }
	}
}