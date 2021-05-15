using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Serialiser;
using System;
using VRViz.Messages.float32[];

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class ChannelFloat32 {
		public std_msgs::string name;
		public std_msgs::float32[] values;

	}
}