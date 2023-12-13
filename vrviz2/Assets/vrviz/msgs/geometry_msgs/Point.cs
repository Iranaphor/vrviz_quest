using std_msgs = VRViz.Messages.std_msgs;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
    
namespace VRViz.Messages.geometry_msgs {
	public class Point {
		public std_msgs::Float64 x;
		public std_msgs::Float64 y;
		public std_msgs::Float64 z;
		public static string ToRosString() { return "geometry_msgs.msg:Point"; }
	}
}


namespace VRViz.Containers {
    public class Pose : VRViz.Containers.Base {
        
        public override string msg_type { get; set; } = "geometry_msgs/Pose";
        public override string reference_type { get; set; } = "rostopic";

        public Pose (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
        
        public override void ApplyMessage() {
            Debug.Log("Applying " + this.mqtt_reference);
        }
    }
}