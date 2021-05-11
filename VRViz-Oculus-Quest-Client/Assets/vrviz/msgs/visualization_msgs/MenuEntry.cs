using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class MenuEntry {
		public std_msgs::UInt32 id;
		public std_msgs::UInt32 parent_id;
		public std_msgs::String title;
		public std_msgs::String command;
		public std_msgs::UInt8 command_type;
	}
}
