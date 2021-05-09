using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class ImageMarker {
		public Header header;
		public std_msgs.String ns;
		public std_msgs.Int32 id;
		public std_msgs.Int32 type;
		public std_msgs.Int32 action;
		public Point position;
		public std_msgs.Float32 scale;
		public ColorRGBA outline_color;
		public std_msgs.UInt8 filled;
		public ColorRGBA fill_color;
		public std_msgs.Duration lifetime;
		public Point[] points;
		public ColorRGBA[] outline_colors;
	}
}
