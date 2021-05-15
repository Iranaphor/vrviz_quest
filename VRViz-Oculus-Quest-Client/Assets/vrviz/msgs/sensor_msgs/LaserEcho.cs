using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using VRViz.Messages.float32[];

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class LaserEcho {
		public std_msgs::float32[] echoes;

	}
}