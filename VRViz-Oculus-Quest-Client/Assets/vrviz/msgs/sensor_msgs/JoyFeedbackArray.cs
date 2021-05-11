using System;
using sensor_msgs = vrviz.msg.sensor_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class JoyFeedbackArray {
		public sensor_msgs::JoyFeedback[] array;
	}
}
