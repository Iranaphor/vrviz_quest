using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.plugins {
	public class LaserScan : rviz_general.Display {

		public bool Selectable;

		public string Style;

		[JsonProperty("Size (m)")]
		public float SizeMeters;

		public float Alpha;

		[JsonProperty("Position Transform")]
		public string PositionTransform;

		[JsonProperty("Color Transform")]
		public string ColorTransform;

		public string Color;

    	[JsonProperty("Decay Time")]
		public float DecayTime = 0f;
    	
    	[JsonProperty("Use Rainbow")]
		public bool UseRainbow = true;
    	
    	[JsonProperty("Invert Rainbow")]
		public bool InvertRainbow = false;
    	
    	[JsonProperty("Min Intensity")]
		public float MinIntensity = 0f;
    	
    	[JsonProperty("Max Intensity")]
		public float MaxIntensity = 4096f;
    	
		public string Axis = "Z";
    	
    	[JsonProperty("Channel Name")]
		public string ChannelName = "intensity";

    }
}
