using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class JoyFeedback {
		public std_msgs::UInt8 type;
		public std_msgs::UInt8 id;
		public std_msgs::Float32 intensity;
		public static string ToRosString() { return "sensor_msgs.msg:JoyFeedback"; }
	}
}