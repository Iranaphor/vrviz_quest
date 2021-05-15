using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class Header {
		public std_msgs::uint32 seq;
		public std_msgs::time stamp;
		public std_msgs::string frame_id;
	}
}