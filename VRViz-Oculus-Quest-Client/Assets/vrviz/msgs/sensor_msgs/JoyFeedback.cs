using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class JoyFeedback {
		public std_msgs::uint8 type;
		public std_msgs::uint8 id;
		public std_msgs::float32 intensity;
	}
}