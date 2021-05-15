using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(DurationConverter))]
	public class Duration{
		public Duration data;
	}
}