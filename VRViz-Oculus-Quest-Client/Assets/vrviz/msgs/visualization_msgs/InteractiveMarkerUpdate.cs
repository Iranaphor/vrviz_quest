using System;
using std_msgs = vrviz.msg.std_msgs;
using visualization_msgs = vrviz.msg.visualization_msgs;

namespace vrviz.msg.visualization_msgs {
	[Serializable]
	public class InteractiveMarkerUpdate {
		public std_msgs::String server_id;
		public std_msgs::UInt64 seq_num;
		public std_msgs::UInt8 type;
		public visualization_msgs::InteractiveMarker[] markers;
		public visualization_msgs::InteractiveMarkerPose[] poses;
		public std_msgs::String[] erases;
	}
}
