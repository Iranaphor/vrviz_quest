using VRViz.Messages.std_msgs;
using VRViz.Messages.string;
using System;
using Newtonsoft.Json;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.geometry_msgs {

	public class TransformStamped {
		public std_msgs::Header header;
		public std_msgs::string child_frame_id;
		public geometry_msgs::Transform transform;

	}
}