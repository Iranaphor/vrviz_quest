using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(TimeConverter))]
	public class Time{
		public Time data;
	}
}