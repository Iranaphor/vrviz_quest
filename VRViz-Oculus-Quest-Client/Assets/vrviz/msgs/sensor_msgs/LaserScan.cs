using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class LaserScan {
		public std_msgs::Header header;
		public std_msgs::float32 angle_min;
		public std_msgs::float32 angle_max;
		public std_msgs::float32 angle_increment;
		public std_msgs::float32 time_increment;
		public std_msgs::float32 scan_time;
		public std_msgs::float32 range_min;
		public std_msgs::float32 range_max;
		public std_msgs::float32[] ranges;
		public std_msgs::float32[] intensities;
	}
}