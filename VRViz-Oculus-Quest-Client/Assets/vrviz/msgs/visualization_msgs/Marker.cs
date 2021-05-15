using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.bool;
using System;
using VRViz.Messages.int32;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;
using VRViz.Messages.std_msgs;
using VRViz.Messages.duration;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.visualization_msgs {

	public class Marker {
		public std_msgs::Header header;
		public std_msgs::string ns;
		public std_msgs::int32 id;
		public std_msgs::int32 type;
		public std_msgs::int32 action;
		public geometry_msgs::Pose pose;
		public geometry_msgs::Vector3 scale;
		public std_msgs::ColorRGBA color;
		public std_msgs::duration lifetime;
		public std_msgs::bool frame_locked;
		public geometry_msgs::Point[] points;
		public std_msgs::ColorRGBA[] colors;
		public std_msgs::string text;
		public std_msgs::string mesh_resource;
		public std_msgs::bool mesh_use_embedded_materials;

	}
}