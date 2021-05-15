using VRViz.Messages.std_msgs;
using Newtonsoft.Json;
using VRViz.Messages.float64[];
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class Float64MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::float64[] data;

	}
}