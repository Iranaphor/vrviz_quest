using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class RegionOfInterest {
		public std_msgs::UInt32 x_offset;
		public std_msgs::UInt32 y_offset;
		public std_msgs::UInt32 height;
		public std_msgs::UInt32 width;
		public std_msgs::Bool do_rectify;
		public static string ToRosString() { return "sensor_msgs.msg:RegionOfInterest"; }
	}
}