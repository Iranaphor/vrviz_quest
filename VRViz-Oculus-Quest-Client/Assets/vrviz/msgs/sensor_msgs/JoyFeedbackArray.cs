using sensor_msgs = VRViz.Messages.sensor_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class JoyFeedbackArray {
		public sensor_msgs::JoyFeedback[] array;
		public static string ToRosString() { return "sensor_msgs.msg:JoyFeedbackArray"; }
	}
}