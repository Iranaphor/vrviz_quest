using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class Float64MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::Float64[] data;
		public static string ToRosString() { return "std_msgs.msg:Float64MultiArray"; }
	}
}