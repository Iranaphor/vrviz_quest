using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.float32;
using System;
using VRViz.Messages.int32;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;
using VRViz.Messages.std_msgs;
using VRViz.Messages.uint8;
using VRViz.Messages.duration;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.visualization_msgs {

	public class ImageMarker {
		public std_msgs::Header header;
		public std_msgs::string ns;
		public std_msgs::int32 id;
		public std_msgs::int32 type;
		public std_msgs::int32 action;
		public geometry_msgs::Point position;
		public std_msgs::float32 scale;
		public std_msgs::ColorRGBA outline_color;
		public std_msgs::uint8 filled;
		public std_msgs::ColorRGBA fill_color;
		public std_msgs::duration lifetime;
		public geometry_msgs::Point[] points;
		public std_msgs::ColorRGBA[] outline_colors;

	}
}