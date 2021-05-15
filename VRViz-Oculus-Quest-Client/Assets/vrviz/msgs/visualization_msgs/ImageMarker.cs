using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class ImageMarker {
		public std_msgs::Header header;
		public std_msgs::String ns;
		public std_msgs::Int32 id;
		public std_msgs::Int32 type;
		public std_msgs::Int32 action;
		public geometry_msgs::Point position;
		public std_msgs::Float32 scale;
		public std_msgs::ColorRGBA outline_color;
		public std_msgs::UInt8 filled;
		public std_msgs::ColorRGBA fill_color;
		public std_msgs::Duration lifetime;
		public geometry_msgs::Point[] points;
		public std_msgs::ColorRGBA[] outline_colors;
	}
}