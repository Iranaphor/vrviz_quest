using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.sensor_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class JoyFeedbackArray {
		public sensor_msgs::JoyFeedback[] array;
	}
}