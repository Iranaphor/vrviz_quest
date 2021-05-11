using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class Temperature {
		public std_msgs::Header header;
		public std_msgs::Float64 temperature;
		public std_msgs::Float64 variance;
	}
}
