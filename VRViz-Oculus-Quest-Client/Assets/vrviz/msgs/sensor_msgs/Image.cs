using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.uint32;
using System;
using VRViz.Serialiser;
using VRViz.Messages.uint8[];
using VRViz.Messages.std_msgs;
using VRViz.Messages.uint8;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class Image {
		public std_msgs::Header header;
		public std_msgs::uint32 height;
		public std_msgs::uint32 width;
		public std_msgs::string encoding;
		public std_msgs::uint8 is_bigendian;
		public std_msgs::uint32 step;
		public std_msgs::uint8[] data;

	}
}