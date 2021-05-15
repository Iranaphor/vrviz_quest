using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class Pose {
		public geometry_msgs::Point position;
		public geometry_msgs::Quaternion orientation;
	}
}