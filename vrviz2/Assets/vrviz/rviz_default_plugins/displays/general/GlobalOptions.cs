using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.general {
    public class GlobalOptions {
        
		[JsonProperty("Background Color")]
        public string BackgroundColor;
        
		[JsonProperty("Fixed Frame")]
        public string FixedFrame;

		[JsonProperty("Frame Rate")]
        public uint FrameRate;
    }
}
