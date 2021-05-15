using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.visualization_msgs {
	public class MenuEntry {
		public std_msgs::UInt32 id;
		public std_msgs::UInt32 parent_id;
		public std_msgs::String title;
		public std_msgs::String command;
		public std_msgs::UInt8 command_type;
	}
}