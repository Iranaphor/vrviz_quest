using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.sensor_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class MultiEchoLaserScan {
		public std_msgs::Header header;
		public std_msgs::Float32 angle_min;
		public std_msgs::Float32 angle_max;
		public std_msgs::Float32 angle_increment;
		public std_msgs::Float32 time_increment;
		public std_msgs::Float32 scan_time;
		public std_msgs::Float32 range_min;
		public std_msgs::Float32 range_max;
		public sensor_msgs::LaserEcho[] ranges;
		public sensor_msgs::LaserEcho[] intensities;
	}
}