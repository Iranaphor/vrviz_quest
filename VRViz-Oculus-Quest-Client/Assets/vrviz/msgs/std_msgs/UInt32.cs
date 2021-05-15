using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(UInt32Converter))]
	public class UInt32{
		public uint data;
	}
}