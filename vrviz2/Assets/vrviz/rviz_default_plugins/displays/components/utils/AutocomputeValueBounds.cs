using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;

using Newtonsoft.Json;
        
namespace VRViz.plugins.rviz_default_plugins.utils {
	public class AutoComputeValueBounds {

		[JsonProperty("Min Value")]
		public int MinValue;

		[JsonProperty("Max Value")]
		public int MaxValue;

		public bool Value;
	}
}


//     Orientation:
//       Alpha: 0.5
//       Color: 255; 255; 127
//       Color Style: Unique
//       Frame: Local
//       Offset: 1
//       Scale: 1
//       Value: true