using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(Int32Converter))]
	public class Int32{
		public int data;
	}
}