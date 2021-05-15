using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class PointCloud2 {
		public std_msgs::Header header;
		public std_msgs::uint32 height;
		public std_msgs::uint32 width;
		public sensor_msgs::PointField[] fields;
		public std_msgs::bool is_bigendian;
		public std_msgs::uint32 point_step;
		public std_msgs::uint32 row_step;
		public std_msgs::uint8[] data;
		public std_msgs::bool is_dense;
	}
}