using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.float32;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.geometry_msgs {

	public class Point32 {
		public std_msgs::float32 x;
		public std_msgs::float32 y;
		public std_msgs::float32 z;

	}
}