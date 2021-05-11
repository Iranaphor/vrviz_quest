using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class Marker {
		public std_msgs::Header header;
		public std_msgs::String ns;
		public std_msgs::Int32 id;
		public std_msgs::Int32 type;
		public std_msgs::Int32 action;
		public geometry_msgs::Pose pose;
		public geometry_msgs::Vector3 scale;
		public std_msgs::ColorRGBA color;
		public std_msgs::Duration lifetime;
		public std_msgs::Bool frame_locked;
		public geometry_msgs::Point[] points;
		public std_msgs::ColorRGBA[] colors;
		public std_msgs::String text;
		public std_msgs::String mesh_resource;
		public std_msgs::Bool mesh_use_embedded_materials;
	}
}
