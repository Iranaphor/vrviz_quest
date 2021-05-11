using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class JoyFeedback {
		public std_msgs::UInt8 type;
		public std_msgs::UInt8 id;
		public std_msgs::Float32 intensity;
	}
}
