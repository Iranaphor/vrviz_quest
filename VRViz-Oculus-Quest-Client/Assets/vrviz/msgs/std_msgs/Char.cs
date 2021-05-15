using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(CharConverter))]
	public class Char{
		public char data;
	}
}