using VRViz.Messages.float64;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.geometry_msgs {

	public class Inertia {
		public std_msgs::float64 m;
		public geometry_msgs::Vector3 com;
		public std_msgs::float64 ixx;
		public std_msgs::float64 ixy;
		public std_msgs::float64 ixz;
		public std_msgs::float64 iyy;
		public std_msgs::float64 iyz;
		public std_msgs::float64 izz;

	}
}