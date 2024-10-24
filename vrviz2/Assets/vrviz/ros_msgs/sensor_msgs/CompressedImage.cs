using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class CompressedImage {
		public std_msgs::Header header;
		public std_msgs::String format;
		[JsonConverter(typeof(UInt8B64Converter))]
		public std_msgs::UInt8[] data;
		public static string ToRosString() { return "sensor_msgs.msg:CompressedImage"; }
	}
}