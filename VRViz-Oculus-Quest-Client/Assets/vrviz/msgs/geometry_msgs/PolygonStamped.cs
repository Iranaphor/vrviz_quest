using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class PolygonStamped {
		public std_msgs::Header header;
		public geometry_msgs::Polygon polygon;
	}
}