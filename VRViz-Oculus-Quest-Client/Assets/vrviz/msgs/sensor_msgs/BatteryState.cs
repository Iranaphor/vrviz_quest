using Newtonsoft.Json;
using VRViz.Serialiser;
using System;

using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Messages.sensor_msgs {
	public class BatteryState {
		public std_msgs::Header header;
		public std_msgs::float32 voltage;
		public std_msgs::float32 current;
		public std_msgs::float32 charge;
		public std_msgs::float32 capacity;
		public std_msgs::float32 design_capacity;
		public std_msgs::float32 percentage;
		public std_msgs::uint8 power_supply_status;
		public std_msgs::uint8 power_supply_health;
		public std_msgs::uint8 power_supply_technology;
		public std_msgs::bool present;
		public std_msgs::float32[] cell_voltage;
		public std_msgs::string location;
		public std_msgs::string serial_number;
	}
}