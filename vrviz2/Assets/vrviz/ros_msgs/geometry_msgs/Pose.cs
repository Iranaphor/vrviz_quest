using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class Pose {
		public geometry_msgs::Point position;
		public geometry_msgs::Quaternion orientation;
		public static string ToRosString() { return "geometry_msgs.msg:Pose"; }
	}
}