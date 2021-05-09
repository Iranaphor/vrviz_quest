using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class LaserEcho {
		public std_msgs.Float32[] echoes;
	}
}
