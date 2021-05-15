using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class Int16MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::Int16[] data;
	}
}