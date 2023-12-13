using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class JointState {
		public std_msgs::Header header;
		public std_msgs::String[] name;
		public std_msgs::Float64[] position;
		public std_msgs::Float64[] velocity;
		public std_msgs::Float64[] effort;
		public static string ToRosString() { return "sensor_msgs.msg:JointState"; }
	}
}