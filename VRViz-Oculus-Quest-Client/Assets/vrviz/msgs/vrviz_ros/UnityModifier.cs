using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.vrviz_ros {
	public class UnityModifier {
		public std_msgs::String type;
		public std_msgs::String target_frame;
		public std_msgs::String component;
		public std_msgs::String modifier;
		public static string ToRosString() { return "vrviz_ros.msg:UnityModifier"; }
	}
}