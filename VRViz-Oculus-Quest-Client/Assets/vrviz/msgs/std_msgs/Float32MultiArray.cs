using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class Float32MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::float32[] data;
	}
}