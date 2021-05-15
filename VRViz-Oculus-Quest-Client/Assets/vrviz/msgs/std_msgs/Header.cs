using Newtonsoft.Json;
using VRViz.Messages.string;
using VRViz.Messages.uint32;
using System;
using VRViz.Messages.time;
using VRViz.Serialiser;

using std_msgs = VRViz.Messages.std_msgs;
namespace VRViz.Messages.std_msgs {

	public class Header {
		public std_msgs::uint32 seq;
		public std_msgs::time stamp;
		public std_msgs::string frame_id;

	}
}