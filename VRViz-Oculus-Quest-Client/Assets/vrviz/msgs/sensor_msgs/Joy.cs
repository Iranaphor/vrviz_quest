using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class Joy {
		public std_msgs::Header header;
		public std_msgs::Float32[] axes;
		public std_msgs::Int32[] buttons;
	}
}