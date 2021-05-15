using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(Int8Converter))]
	public class Int8{
		public sbyte data;
	}
}