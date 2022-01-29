using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using sensor_msgs = VRViz.Messages.sensor_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;

using VRViz.Modifiers;

namespace VRViz.Containers {

    public class Image : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "sensor_msgs/Image";
        public override string reference_type { get; set; } = "rostopic";

        public sensor_msgs::Image message_data { get; set; }
        public GameObject frame;
        public RawImage raw_image;


        public Image (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {
            this.frame = GameObject.Find("Display_3");
            this.raw_image = this.frame.GetComponent(typeof(RawImage)) as RawImage;
        }

        public override void ApplyMessage() {
            Debug.Log("Applying");
            VRViz.Modifiers.ApplyMessage.SetImage(message_data, this.raw_image);



        }
    }


    public class Map : VRViz.Containers.Base {
        public override string msg_type { get; set; } = "nav_msgs/OccupancyGrid";
        public override string reference_type { get; set; } = "rostopic";

        public nav_msgs::OccupancyGrid message_data { get; set; } = null;

        public Map (string R, Dictionary<string, object> display_details, Dictionary<string, string> T) : base(R, T) {}
        public override void ApplyMessage() { Debug.Log("Applying"); }
    }
}