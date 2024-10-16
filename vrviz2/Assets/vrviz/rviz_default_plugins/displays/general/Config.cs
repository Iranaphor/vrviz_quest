using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.general {
	public class Config {

		[JsonIgnore]
		public string Panels;

		[JsonProperty("Visualization Manager")]
		public rviz_general::VisualizationManager VisualizationManager;

		[JsonIgnore]
		public string WindowGeometry;
   }
}
