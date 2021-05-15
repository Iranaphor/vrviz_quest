using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.uint64;
using VRViz.Messages.string[];
using System;
using VRViz.Serialiser;
using VRViz.Messages.visualization_msgs;
using VRViz.Messages.uint8;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.visualization_msgs {

	public class InteractiveMarkerUpdate {
		public std_msgs::string server_id;
		public std_msgs::uint64 seq_num;
		public std_msgs::uint8 type;
		public visualization_msgs::InteractiveMarker[] markers;
		public visualization_msgs::InteractiveMarkerPose[] poses;
		public std_msgs::string[] erases;

	}
}