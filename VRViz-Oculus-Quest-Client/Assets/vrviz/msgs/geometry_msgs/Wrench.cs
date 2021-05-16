using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class Wrench {
		public geometry_msgs::Vector3 force;
		public geometry_msgs::Vector3 torque;
		public static string ToRosString() { return "geometry_msgs.msg:Wrench"; }
	}
}