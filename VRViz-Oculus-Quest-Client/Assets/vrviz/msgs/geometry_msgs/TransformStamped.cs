using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class TransformStamped {
		public Header header;
		public std_msgs.String child_frame_id;
		public Transform transform;
	}
}
