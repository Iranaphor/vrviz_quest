using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class Quaternion {
		public std_msgs::Float64 x;
		public std_msgs::Float64 y;
		public std_msgs::Float64 z;
		public std_msgs::Float64 w;
	}
}