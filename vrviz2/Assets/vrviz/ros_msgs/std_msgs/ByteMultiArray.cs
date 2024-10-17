using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class ByteMultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::Byte[] data;
		public static string ToRosString() { return "std_msgs.msg:ByteMultiArray"; }
	}
}