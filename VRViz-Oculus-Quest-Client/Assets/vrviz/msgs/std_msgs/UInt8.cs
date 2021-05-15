using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(UInt8Converter))]
	public class UInt8{
		public byte data;
	}
}