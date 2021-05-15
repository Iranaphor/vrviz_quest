using Newtonsoft.Json;
using VRViz.Messages.string;
using System;
using VRViz.Messages.time;
using VRViz.Serialiser;
using VRViz.Messages.std_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class TimeReference {
		public std_msgs::Header header;
		public std_msgs::time time_ref;
		public std_msgs::string source;

	}
}