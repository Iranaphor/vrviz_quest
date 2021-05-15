using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(Int64Converter))]
	public class Int64{
		public long data;
	}
}