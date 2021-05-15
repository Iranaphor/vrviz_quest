using Newtonsoft.Json;
using VRViz.Messages.int32[];
using System;
using VRViz.Messages.float32[];
using VRViz.Serialiser;
using VRViz.Messages.std_msgs;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class Joy {
		public std_msgs::Header header;
		public std_msgs::float32[] axes;
		public std_msgs::int32[] buttons;

	}
}