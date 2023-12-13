using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.vrviz_ros {
	public class MqttParameters {
		public std_msgs::String control_topic;
		public std_msgs::String mqtt_reference;
		public std_msgs::Float64 frequency;
		public std_msgs::Bool latched;
		public std_msgs::Int8 qos;
		public static string ToRosString() { return "vrviz_ros.msg:MqttParameters"; }

		// INTERNAL USE ONLY. MqttParameters SHOULD BE INSTANTIATED VIA THE VRViz ROS CONFIG FILE. 
		public MqttParameters(){
			this.control_topic = new std_msgs.String();
			this.control_topic.data = "__dynamic_server";

			this.mqtt_reference = new std_msgs.String();
			this.mqtt_reference.data = "vrviz/vrviz/config";

			this.frequency = new std_msgs.Float64();
			this.frequency.data = 1.0;
			
			this.latched = new std_msgs.Bool();
			this.latched.data = true; //default=true for use in DecodeMessages/config (general default=false)

			this.qos = new std_msgs.Int8();
			this.qos.data = 2;
		}
	}
}