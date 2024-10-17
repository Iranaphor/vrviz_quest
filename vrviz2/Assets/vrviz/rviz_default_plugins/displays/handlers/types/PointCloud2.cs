using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.plugins {
	public class PointCloud2 : rviz_general.Display {


        [JsonProperty("Selectable")]
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
        public float DecayTime;

        [JsonProperty("Use Rainbow")]
        public bool UseRainbow;

        [JsonProperty("Invert Rainbow")]
        public bool InvertRainbow;

        [JsonProperty("Min Intensity")]
        public float MinIntensity;

        [JsonProperty("Max Intensity")]
        public float MaxIntensity;

        public string Axis;

        [JsonProperty("Channel Name")]
        public string ChannelName;

        [JsonProperty("Size (Pixels)")]
        public int SizePixels;

        [JsonProperty("Autocompute Intensity Bounds")]
        public bool AutocomputeIntensityBounds;

        [JsonProperty("Autocompute Value Bounds")]
        public rviz_utils::AutoComputeValueBounds AutocomputeValueBounds;

        [JsonProperty("Use Fixed Frame")]
        public bool UseFixedFrame;

    }
}
