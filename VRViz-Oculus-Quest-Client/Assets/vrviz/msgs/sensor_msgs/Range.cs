using Newtonsoft.Json;
using VRViz.Messages.float32;
using System;
using VRViz.Serialiser;
using VRViz.Messages.std_msgs;
using VRViz.Messages.uint8;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class Range {
		public std_msgs::Header header;
		public std_msgs::uint8 radiation_type;
		public std_msgs::float32 field_of_view;
		public std_msgs::float32 min_range;
		public std_msgs::float32 max_range;
		public std_msgs::float32 range;

	}
}