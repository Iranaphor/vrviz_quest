using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class MultiArrayDimension {
		public std_msgs::String label;
		public std_msgs::UInt32 size;
		public std_msgs::UInt32 stride;
		public static string ToRosString() { return "std_msgs.msg:MultiArrayDimension"; }
	}
}