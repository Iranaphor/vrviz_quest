using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(UInt64Converter))]
	public class UInt64{
		public ulong data;
	}
}