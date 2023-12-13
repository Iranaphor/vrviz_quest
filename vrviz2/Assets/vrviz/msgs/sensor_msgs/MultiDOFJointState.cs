using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class MultiDOFJointState {
		public std_msgs::Header header;
		public std_msgs::String[] joint_names;
		public geometry_msgs::Transform[] transforms;
		public geometry_msgs::Twist[] twist;
		public geometry_msgs::Wrench[] wrench;
		public static string ToRosString() { return "sensor_msgs.msg:MultiDOFJointState"; }
	}
}