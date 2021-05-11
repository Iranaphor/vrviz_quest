using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Illuminance {
		public std_msgs::Header header;
		public std_msgs::Float64 illuminance;
		public std_msgs::Float64 variance;
	}
}
