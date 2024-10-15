using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.plugins {
	public class LaserScan : rviz_general.Display {

		public bool Selectable;

		public string Style;

		[JsonProperty("Size (m)")]
		public float Size;

		public uint Alpha;

		[JsonProperty("Decay Time")]
		public uint DecayTime;

		[JsonProperty("Position Transform")]
		public string PositionTransform;

		[JsonProperty("Color Transform")]
		public string ColorTransform;

		public string Color;

    }
}
