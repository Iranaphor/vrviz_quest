using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(Float32Converter))]
	public class Float32{
		public float data;
	}
}