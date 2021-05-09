using System;
using vrviz.msg.std_msgs;
using vrviz.msg.geometry_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class MultiDOFJointState {
		public Header header;
		public std_msgs.String[] joint_names;
		public Transform[] transforms;
		public Twist[] twist;
		public Wrench[] wrench;
	}
}
