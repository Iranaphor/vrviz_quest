using System;
using Newtonsoft.Json;
using VRViz.Serialiser;

namespace VRViz.Messages.std_msgs {
	[JsonConverter(typeof(UInt16Converter))]
	public class UInt16{
		public ushort data;
	public static string ToRosString() { return "std_msgs.msg:UInt16"; }
	}
}