using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.plugins {
	public class Map : rviz_general.Display {

        public float Alpha;

		[JsonProperty("Color Scheme")]
        public string ColorScheme;

		[JsonProperty("Draw Behind")]
        public bool DrawBehind;

		[JsonProperty("Update Topic")]
		public rviz_utils::Topic UpdateTopic;

		[JsonProperty("Use Timestamp")]
		public bool UseTimestamp;

    }
}
