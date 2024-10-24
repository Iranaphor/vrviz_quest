using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class LaserEcho {
		public std_msgs::Float32[] echoes;
		public static string ToRosString() { return "sensor_msgs.msg:LaserEcho"; }
	}
}