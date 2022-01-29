using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using sensor_msgs = VRViz.Messages.sensor_msgs;

namespace VRViz.Containers {

    public class Laserscan : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "sensor_msgs/LaserScan";
        public override string reference_type { get; set; } = "rostopic";

        public Laserscan (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public sensor_msgs::LaserScan message_data { get; set; }
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class PointCloud2 : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "sensor_msgs/PointCloud2";
        public override string reference_type { get; set; } = "rostopic";

        public PointCloud2 (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public sensor_msgs::PointCloud2 message_data { get; set; }
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

}