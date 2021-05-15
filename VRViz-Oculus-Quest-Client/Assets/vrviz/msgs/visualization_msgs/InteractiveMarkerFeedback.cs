using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.uint32;
using VRViz.Messages.bool;
using System;
using VRViz.Serialiser;
using VRViz.Messages.geometry_msgs;
using VRViz.Messages.std_msgs;
using VRViz.Messages.uint8;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.visualization_msgs {

	public class InteractiveMarkerFeedback {
		public std_msgs::Header header;
		public std_msgs::string client_id;
		public std_msgs::string marker_name;
		public std_msgs::string control_name;
		public std_msgs::uint8 event_type;
		public geometry_msgs::Pose pose;
		public std_msgs::uint32 menu_entry_id;
		public geometry_msgs::Point mouse_point;
		public std_msgs::bool mouse_point_valid;

	}
}