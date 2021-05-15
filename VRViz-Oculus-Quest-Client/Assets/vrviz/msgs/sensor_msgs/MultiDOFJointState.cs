using Newtonsoft.Json;
using VRViz.Messages.geometry_msgs;
using VRViz.Messages.string[];
using System;
using VRViz.Serialiser;
using VRViz.Messages.std_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class MultiDOFJointState {
		public std_msgs::Header header;
		public std_msgs::string[] joint_names;
		public geometry_msgs::Transform[] transforms;
		public geometry_msgs::Twist[] twist;
		public geometry_msgs::Wrench[] wrench;

	}
}