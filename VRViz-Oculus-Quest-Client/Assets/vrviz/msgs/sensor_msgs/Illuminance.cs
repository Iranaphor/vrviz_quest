using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class Illuminance {
		public std_msgs::Header header;
		public std_msgs::float64 illuminance;
		public std_msgs::float64 variance;
	}
}