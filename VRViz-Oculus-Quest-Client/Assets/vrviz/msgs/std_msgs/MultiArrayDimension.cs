using VRViz.Messages.uint32;
using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class MultiArrayDimension {
		public std_msgs::string label;
		public std_msgs::uint32 size;
		public std_msgs::uint32 stride;

	}
}