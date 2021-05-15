using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.uint64;
using System;
using VRViz.Serialiser;
using VRViz.Messages.visualization_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.visualization_msgs {

	public class InteractiveMarkerInit {
		public std_msgs::string server_id;
		public std_msgs::uint64 seq_num;
		public visualization_msgs::InteractiveMarker[] markers;

	}
}