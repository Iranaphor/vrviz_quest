using VRViz.Messages.std_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.int64[];

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class Int64MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::int64[] data;

	}
}