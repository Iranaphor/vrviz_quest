using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.msg.std_msgs{
	[JsonConverter(typeof(Float64Converter))]
	public class Float64{
		public double data;
	}
}