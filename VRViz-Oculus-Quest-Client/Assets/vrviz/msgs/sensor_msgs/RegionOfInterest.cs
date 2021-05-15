using VRViz.Messages.bool;
using VRViz.Messages.uint32;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class RegionOfInterest {
		public std_msgs::uint32 x_offset;
		public std_msgs::uint32 y_offset;
		public std_msgs::uint32 height;
		public std_msgs::uint32 width;
		public std_msgs::bool do_rectify;

	}
}