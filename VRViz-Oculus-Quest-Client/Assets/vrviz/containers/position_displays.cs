using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRViz.Containers {

    public class Pose : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "geometry_msgs/Pose";
        public override string reference_type { get; set; } = "rostopic";

        public Pose (string topic, Dictionary<string, object> details) : base(topic) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class PoseArray : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "geometry_msgs/PoseArray";
        public override string reference_type { get; set; } = "rostopic";

        public PoseArray (string topic, Dictionary<string, object> details) : base(topic) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class Odometry : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "nav_msgs/Odometry";
        public override string reference_type { get; set; } = "rosparam";

        public Odometry (string topic, Dictionary<string, object> details) : base(topic) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

}