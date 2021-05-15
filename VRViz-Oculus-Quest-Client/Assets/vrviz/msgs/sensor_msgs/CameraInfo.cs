using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class CameraInfo {
		public std_msgs::Header header;
		public std_msgs::uint32 height;
		public std_msgs::uint32 width;
		public std_msgs::string distortion_model;
		public std_msgs::float64[] D;
		public std_msgs::float64[9] K;
		public std_msgs::float64[9] R;
		public std_msgs::float64[12] P;
		public std_msgs::uint32 binning_x;
		public std_msgs::uint32 binning_y;
		public sensor_msgs::RegionOfInterest roi;
	}
}