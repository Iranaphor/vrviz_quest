using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class MultiArrayLayout {
		public std_msgs::MultiArrayDimension[] dim;
		public std_msgs::uint32 data_offset;
	}
}