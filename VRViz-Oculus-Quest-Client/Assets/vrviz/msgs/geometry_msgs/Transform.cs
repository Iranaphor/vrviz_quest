using VRViz.Messages.geometry_msgs;
using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.geometry_msgs {
	public class Transform {
		public geometry_msgs::Vector3 translation;
		public geometry_msgs::Quaternion rotation;
	}
}