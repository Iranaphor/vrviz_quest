using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class FluidPressure {
		public std_msgs::Header header;
		public std_msgs::Float64 fluid_pressure;
		public std_msgs::Float64 variance;
		public static string ToRosString() { return "sensor_msgs.msg:FluidPressure"; }
	}
}