using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class Inertia {
		public std_msgs::Float64 m;
		public geometry_msgs::Vector3 com;
		public std_msgs::Float64 ixx;
		public std_msgs::Float64 ixy;
		public std_msgs::Float64 ixz;
		public std_msgs::Float64 iyy;
		public std_msgs::Float64 iyz;
		public std_msgs::Float64 izz;
		public static string ToRosString() { return "geometry_msgs.msg:Inertia"; }
	}
}