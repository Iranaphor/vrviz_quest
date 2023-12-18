using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.utils {
	public class Shape {

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

		public string Value;
	}
}

			// Alpha: 1
			// Axes Length: 1
			// Axes Radius: 0.10000000149011612
			// Color: 255; 25; 0
			// Head Length: 0.30000001192092896
			// Head Radius: 0.10000000149011612
			// Shaft Length: 1
			// Shaft Radius: 0.05000000074505806
			// Value: Arrow