using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class MultiDOFJointState {
		public std_msgs::Header header;
		public std_msgs::String[] joint_names;
		public geometry_msgs::Transform[] transforms;
		public geometry_msgs::Twist[] twist;
		public geometry_msgs::Wrench[] wrench;
	}
}
