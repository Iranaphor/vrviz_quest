using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class UInt32MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::uint32[] data;
	}
}