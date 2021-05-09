using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class FluidPressure {
		public Header header;
		public std_msgs.Float64 fluid_pressure;
		public std_msgs.Float64 variance;
	}
}
