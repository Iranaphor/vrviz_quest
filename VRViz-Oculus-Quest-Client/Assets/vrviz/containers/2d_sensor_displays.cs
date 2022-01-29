using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using nav_msgs = VRViz.Messages.nav_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;
using std_msgs = VRViz.Messages.std_msgs;

namespace VRViz.Containers {

    public class Path : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "nav_msgs/Path";
        public override string reference_type { get; set; } = "rostopic";

        public Path (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public nav_msgs::Path message_data { get; set; }
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class Polygon : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "geometry_msgs/Polygon";
        public override string reference_type { get; set; } = "rostopic";

        public Polygon (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public geometry_msgs::Polygon message_data { get; set; }
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }

    public class RobotModel : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "std_msgs/String";
        public override string reference_type { get; set; } = "rostopic";
//        public override string msg_type { get; set; } = "vrviz_msgs/urdf";
//        public override string reference_type { get; set; } = "rosparam";

        public RobotModel (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}

        //Apply the message contents to the scene
        public std_msgs::String message_data { get; set; }
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }
}