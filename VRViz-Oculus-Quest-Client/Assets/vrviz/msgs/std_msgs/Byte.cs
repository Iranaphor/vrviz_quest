using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(ByteConverter))]
	public class Byte{
		public byte data;
	}
}