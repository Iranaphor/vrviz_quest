using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class Range {
		public std_msgs::Header header;
		public std_msgs::UInt8 radiation_type;
		public std_msgs::Float32 field_of_view;
		public std_msgs::Float32 min_range;
		public std_msgs::Float32 max_range;
		public std_msgs::Float32 range;
		public static string ToRosString() { return "sensor_msgs.msg:Range"; }
	}
}