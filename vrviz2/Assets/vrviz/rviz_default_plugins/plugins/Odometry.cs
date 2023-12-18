using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;

using Newtonsoft.Json;

namespace VRViz.plugins.rviz_default_plugins.plugins {
	public class Odometry : rviz_general.Display {


		[JsonProperty("Angle Tolerance")]
		public float AngleTolerance;
      	
		public rviz_utils::Covariance Covariance;
      	
		public uint Keep;

		[JsonProperty("Position Tolerance")]
		public float PositionTolerance;

		public rviz_utils::Shape Shape;

      	public bool Value;

    }
}
