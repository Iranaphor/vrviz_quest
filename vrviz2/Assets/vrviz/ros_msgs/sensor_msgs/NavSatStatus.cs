using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class NavSatStatus {
		public std_msgs::Int8 status;
		public std_msgs::UInt16 service;
		public static string ToRosString() { return "sensor_msgs.msg:NavSatStatus"; }
	}
}