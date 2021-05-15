using VRViz.Messages.std_msgs;
using Newtonsoft.Json;
using VRViz.Messages.int32[];
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class Int32MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::int32[] data;

	}
}