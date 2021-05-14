using System;
using Newtonsoft.Json;
using VRViz.serialisers;

namespace vrviz.msg.std_msgs {
	[Serializable]
	[JsonConverter(typeof(UInt8Converter))]
	public class UInt8 {
		public byte data;
	}
}
