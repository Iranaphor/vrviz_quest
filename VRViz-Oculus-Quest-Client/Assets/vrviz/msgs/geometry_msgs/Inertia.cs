using System;
using geometry_msgs = vrviz.msg.geometry_msgs;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.geometry_msgs {
	[Serializable]
	public class Inertia {
		public std_msgs::Float64 m;
		public geometry_msgs::Vector3 com;
		public std_msgs::Float64 ixx;
		public std_msgs::Float64 ixy;
		public std_msgs::Float64 ixz;
		public std_msgs::Float64 iyy;
		public std_msgs::Float64 iyz;
		public std_msgs::Float64 izz;
	}
}
