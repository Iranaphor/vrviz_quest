using System;
using vrviz.msg.std_msgs;

namespace vrviz.msg.sensor_msgs {
	[Serializable]
	public class BatteryState {
		public Header header;
		public std_msgs.Float32 voltage;
		public std_msgs.Float32 current;
		public std_msgs.Float32 charge;
		public std_msgs.Float32 capacity;
		public std_msgs.Float32 design_capacity;
		public std_msgs.Float32 percentage;
		public std_msgs.UInt8 power_supply_status;
		public std_msgs.UInt8 power_supply_health;
		public std_msgs.UInt8 power_supply_technology;
		public std_msgs.Bool present;
		public std_msgs.Float32[] cell_voltage;
		public std_msgs.String location;
		public std_msgs.String serial_number;
	}
}
