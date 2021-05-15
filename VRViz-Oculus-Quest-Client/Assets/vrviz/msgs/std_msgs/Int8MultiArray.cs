using VRViz.Messages.std_msgs;
using Newtonsoft.Json;
using VRViz.Messages.int8[];
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class Int8MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::int8[] data;

	}
}