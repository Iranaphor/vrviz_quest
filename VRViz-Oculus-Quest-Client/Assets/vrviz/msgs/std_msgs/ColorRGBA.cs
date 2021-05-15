using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.std_msgs {
	public class ColorRGBA {
		public std_msgs::float32 r;
		public std_msgs::float32 g;
		public std_msgs::float32 b;
		public std_msgs::float32 a;
	}
}