using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(Float64Converter))]
	public class Float64{
		public double data;
	public static string ToRosString() { return "std_msgs.msg:Float64"; }
	}
}