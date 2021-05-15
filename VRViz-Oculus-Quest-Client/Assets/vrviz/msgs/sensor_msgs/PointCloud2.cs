using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.sensor_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class PointCloud2 {
		public std_msgs::Header header;
		public std_msgs::UInt32 height;
		public std_msgs::UInt32 width;
		public sensor_msgs::PointField[] fields;
		public std_msgs::Bool is_bigendian;
		public std_msgs::UInt32 point_step;
		public std_msgs::UInt32 row_step;
		public std_msgs::UInt8[] data;
		public std_msgs::Bool is_dense;
	}
}