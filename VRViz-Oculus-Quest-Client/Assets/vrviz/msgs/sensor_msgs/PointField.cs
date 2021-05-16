using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class PointField {
		public std_msgs::String name;
		public std_msgs::UInt32 offset;
		public std_msgs::UInt8 datatype;
		public std_msgs::UInt32 count;
		public static string ToRosString() { return "sensor_msgs.msg:PointField"; }
	}
}