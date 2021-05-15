using VRViz.Messages.int8;
using Newtonsoft.Json;
using VRViz.Messages.uint16;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class NavSatStatus {
		public std_msgs::int8 status;
		public std_msgs::uint16 service;

	}
}