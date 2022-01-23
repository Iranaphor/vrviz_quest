using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using vrviz_ros = VRViz.Messages.vrviz_ros;
using geometry_msgs = VRViz.Messages.geometry_msgs;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.vrviz_ros {
	public class SystemConfig {
		public vrviz_ros::Topic[] topic_list; 
		public geometry_msgs::TransformStamped[] frame_list;
		public static string ToRosString() { return "vrviz_ros.msg:SystemConfig"; }
	}
}