using VRViz.Messages.float64;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.geometry_msgs {

	public class Vector3 {
		public std_msgs::float64 x;
		public std_msgs::float64 y;
		public std_msgs::float64 z;

	}
}