using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRViz.Containers {

    public class Path : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "nav_msgs/Path";
        public override string reference_type { get; set; } = "rostopic";

        public Path (string topic, Dictionary<string, object> details) : base(topic) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class Polygon : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "geometry_msgs/Polygon";
        public override string reference_type { get; set; } = "rostopic";

        public Polygon (string topic, Dictionary<string, object> details) : base(topic) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class RobotModel : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "URDF";
        public override string reference_type { get; set; } = "rosparam";

        public RobotModel (string topic, Dictionary<string, object> details) : base(topic) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

}