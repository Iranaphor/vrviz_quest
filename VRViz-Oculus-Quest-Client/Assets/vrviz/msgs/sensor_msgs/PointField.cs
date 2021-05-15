using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.uint32;
using System;
using VRViz.Serialiser;
using VRViz.Messages.uint8;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.sensor_msgs {

	public class PointField {
		public std_msgs::string name;
		public std_msgs::uint32 offset;
		public std_msgs::uint8 datatype;
		public std_msgs::uint32 count;

	}
}