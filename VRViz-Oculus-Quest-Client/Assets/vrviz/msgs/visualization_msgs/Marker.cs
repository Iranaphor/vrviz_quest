using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class Marker {
		public Header header;
		public std_msgs.String ns;
		public std_msgs.Int32 id;
		public std_msgs.Int32 type;
		public std_msgs.Int32 action;
		public Pose pose;
		public Vector3 scale;
		public ColorRGBA color;
		public std_msgs.Duration lifetime;
		public std_msgs.Bool frame_locked;
		public Point[] points;
		public ColorRGBA[] colors;
		public std_msgs.String text;
		public std_msgs.String mesh_resource;
		public std_msgs.Bool mesh_use_embedded_materials;
	}
}
