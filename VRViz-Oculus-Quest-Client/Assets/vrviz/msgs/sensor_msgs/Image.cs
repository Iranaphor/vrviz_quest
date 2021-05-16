using Newtonsoft.Json;
using VRViz.Serialiser;
using System;
using System.Collections;
using System.Collections.Generic;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class Image {
		public std_msgs::Header header;
		public std_msgs::UInt32 height;
		public std_msgs::UInt32 width;
		public std_msgs::String encoding;
		public std_msgs::UInt8 is_bigendian;
		public std_msgs::UInt32 step;
		[JsonConverter(typeof(UInt8ArrayConverter))]
		public std_msgs::UInt8[] data;
		public static string ToRosString() { return "sensor_msgs.msg:Image"; }
	


/**
data_arr = new UInt8[string.length]
for character in string{
	data_arr[position of character] = Convert.ToByte(character)
}
return



*/}
}