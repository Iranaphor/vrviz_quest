using VRViz.Messages.uint8[];
using VRViz.Messages.std_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class UInt8MultiArray {
		public std_msgs::MultiArrayLayout layout;
		public std_msgs::uint8[] data;

	}
}