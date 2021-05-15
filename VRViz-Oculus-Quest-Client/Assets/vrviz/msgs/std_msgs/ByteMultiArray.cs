using VRViz.Messages.std_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.byte[];

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class ByteMultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::byte[] data;

	}
}