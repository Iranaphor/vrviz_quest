using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class TimeReference {
		public std_msgs::Header header;
		public std_msgs::Time time_ref;
		public std_msgs::String source;
		public static string ToRosString() { return "sensor_msgs.msg:TimeReference"; }
	}
}