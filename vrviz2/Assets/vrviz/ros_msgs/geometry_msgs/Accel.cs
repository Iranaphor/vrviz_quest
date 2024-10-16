using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class Accel {
		public geometry_msgs::Vector3 linear;
		public geometry_msgs::Vector3 angular;
		public static string ToRosString() { return "geometry_msgs.msg:Accel"; }
	}
}