using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.plugins {
	public class PointStamped : rviz_general.Display {

		public float Alpha;

		public string Color;

		[JsonProperty("History Length")]
		public uint HistoryLength;

		public uint Radius;
    }
}

    // - Alpha: 1
    //   Class: rviz_default_plugins/PointStamped
    //   Color: 204; 41; 204
    //   Enabled: true
    //   History Length: 1
    //   Name: PointStamped
    //   Radius: 0.20000000298023224
    //   Topic:
    //     Depth: 5
    //     Durability Policy: Volatile
    //     Filter size: 10
    //     History Policy: Keep Last
    //     Reliability Policy: Reliable
    //     Value: /clicked_point
    //     Value: true