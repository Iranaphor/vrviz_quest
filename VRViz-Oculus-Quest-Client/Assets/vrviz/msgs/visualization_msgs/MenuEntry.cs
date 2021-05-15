using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.uint32;
using System;
using VRViz.Serialiser;
using VRViz.Messages.uint8;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.visualization_msgs {

	public class MenuEntry {
		public std_msgs::uint32 id;
		public std_msgs::uint32 parent_id;
		public std_msgs::string title;
		public std_msgs::string command;
		public std_msgs::uint8 command_type;

	}
}