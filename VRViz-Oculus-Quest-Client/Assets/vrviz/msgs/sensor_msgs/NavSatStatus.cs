using System;
using std_msgs = vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class NavSatStatus {
		public std_msgs::Int8 status;
		public std_msgs::UInt16 service;
	}
}
