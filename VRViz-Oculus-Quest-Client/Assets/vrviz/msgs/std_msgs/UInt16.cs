using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(UInt16Converter))]
	public class UInt16{
		public ushort data;
	}
}