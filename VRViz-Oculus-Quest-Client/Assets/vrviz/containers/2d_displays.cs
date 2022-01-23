using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRViz.Containers {


    public class Image : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "sensor_msgs/Image";
        public override string reference_type { get; set; } = "rostopic";

        //Apply the message contents to the scene
        public Image (string topic, Dictionary<string, object> details) : base(topic) {}

        public override void ApplyMessage() { Debug.Log("Applying"); }
    }


    public class Map : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "nav_msgs/OccupancyGrid";
        public override string reference_type { get; set; } = "rostopic";

        public Map (string topic, Dictionary<string, object> details) : base(topic) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }

    }
}