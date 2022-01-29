using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRViz.Containers {

    public class Path : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "nav_msgs/Path";
        public override string reference_type { get; set; } = "rostopic";

        public Path (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class Polygon : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "geometry_msgs/Polygon";
        public override string reference_type { get; set; } = "rostopic";

        public Polygon (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class RobotModel : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "std_msgs/String";
        public override string reference_type { get; set; } = "rostopic";
//        public override string msg_type { get; set; } = "vrviz_msgs/urdf";
//        public override string reference_type { get; set; } = "rosparam";

        public RobotModel (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

}