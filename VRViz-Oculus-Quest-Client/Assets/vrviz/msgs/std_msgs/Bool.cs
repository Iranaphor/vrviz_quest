using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(BoolConverter))]
	public class Bool{
		public bool data;
	}
}