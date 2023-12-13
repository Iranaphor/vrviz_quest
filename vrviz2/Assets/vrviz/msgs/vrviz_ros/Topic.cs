using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using vrviz_ros = VRViz.Messages.vrviz_ros;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.vrviz_ros {
	public class Topic {
		public std_msgs::String topic;
		public std_msgs::String msg_type;
		public vrviz_ros::UnityModifier unity;
		public vrviz_ros::MqttParameters mqtt;
		public static string ToRosString() { return "vrviz_ros.msg:Topic"; }
	}
}