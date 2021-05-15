using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class Temperature {
		public std_msgs::Header header;
		public std_msgs::Float64 temperature;
		public std_msgs::Float64 variance;
	}
}