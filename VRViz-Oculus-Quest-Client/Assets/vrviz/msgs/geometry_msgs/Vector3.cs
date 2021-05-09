using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Vector3 {
		public std_msgs.Float64 x;
		public std_msgs.Float64 y;
		public std_msgs.Float64 z;
	}
}
