using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.plugins {
	public class Pose : rviz_general.Display {

		public float Alpha;

		[JsonProperty("Axes Length")]
		public float AxesLength;

		[JsonProperty("Axes Radius")]
		public float AxesRadius;

		public string Color;

		[JsonProperty("Head Length")]
		public float HeadLength;

		[JsonProperty("Head Radius")]
		public float HeadRadius;

		[JsonProperty("Shaft Length")]
		public float ShaftLength;

		[JsonProperty("Shaft Radius")]
		public float ShaftRadius;

		public string Shape;
    }
}


//   - Alpha: 1
//     Axes Length: 1
//     Axes Radius: 0.10000000149011612
//     Class: rviz_default_plugins/Pose
//     Color: 255; 25; 0
//     Enabled: true
//     Head Length: 0.30000001192092896
//     Head Radius: 0.10000000149011612
//     Name: Pose
//     Shaft Length: 1
//     Shaft Radius: 0.05000000074505806
//     Shape: Arrow
//     Topic: ...
//     Value: true
