using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;


namespace VRViz.plugins.rviz_default_plugins.general {
	public class VisualizationManager {

		[JsonIgnore]
		public string Class;

		public rviz_general::Display[] Displays;
		
		[JsonProperty("Global Options")]
		public rviz_general::GlobalOptions GlobalOptions;

		[JsonIgnore]
		public bool Enabled;

		[JsonIgnore]
		public string Name;

		[JsonIgnore]
		public string Tools;

		[JsonIgnore]
		public string Transformation;

		[JsonIgnore]
		public bool Value;

		[JsonIgnore]
		public string Views;
   }
}
