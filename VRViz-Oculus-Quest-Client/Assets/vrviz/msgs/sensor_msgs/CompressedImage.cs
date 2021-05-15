using Newtonsoft.Json;
using VRViz.Messages.string;
using System;
using VRViz.Serialiser;
using VRViz.Messages.uint8[];
using VRViz.Messages.std_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class CompressedImage {
		public std_msgs::Header header;
		public std_msgs::string format;
		public std_msgs::uint8[] data;

	}
}