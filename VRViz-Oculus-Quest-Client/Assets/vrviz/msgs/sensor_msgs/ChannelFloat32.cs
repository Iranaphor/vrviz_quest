using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class ChannelFloat32 {
		public std_msgs::String name;
		public std_msgs::Float32[] values;
		public static string ToRosString() { return "sensor_msgs.msg:ChannelFloat32"; }
	}
}