using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.sensor_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class PointCloud {
		public std_msgs::Header header;
		public geometry_msgs::Point32[] points;
		public sensor_msgs::ChannelFloat32[] channels;
	}
}