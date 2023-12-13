using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;

using Newtonsoft.Json;
        
namespace VRViz.plugins.rviz_default_plugins.utils {
	public class Orientation {

		public float Alpha;

		public string Color;

		[JsonProperty("Color Style")]
        public string ColorStyle;

        public string Frame;

		public float Offset;

		public float Scale;

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