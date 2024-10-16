using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.utils {
	public class Topic {

		public uint Depth;

		[JsonProperty("Durability Policy")]
		public string DurabilityPolicy;

		[JsonProperty("Filter Size")]
		public uint FilterSize;

		[JsonProperty("History Policy")]
		public string HistoryPolicy;

		[JsonProperty("Reliability Policy")]
		public string ReliabilityPolicy;

		public string Value;
	}
}