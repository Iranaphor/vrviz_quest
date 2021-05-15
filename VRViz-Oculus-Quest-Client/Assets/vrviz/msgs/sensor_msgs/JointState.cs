using Newtonsoft.Json;
using VRViz.Messages.string[];
using System;
using VRViz.Messages.float64[];
using VRViz.Serialiser;
using VRViz.Messages.std_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class JointState {
		public std_msgs::Header header;
		public std_msgs::string[] name;
		public std_msgs::float64[] position;
		public std_msgs::float64[] velocity;
		public std_msgs::float64[] effort;

	}
}