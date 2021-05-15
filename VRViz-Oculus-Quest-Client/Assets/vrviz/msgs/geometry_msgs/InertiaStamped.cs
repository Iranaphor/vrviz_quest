using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class InertiaStamped {
		public std_msgs::Header header;
		public geometry_msgs::Inertia inertia;
	}
}