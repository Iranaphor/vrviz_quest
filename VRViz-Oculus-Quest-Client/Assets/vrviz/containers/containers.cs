using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace VRViz.Containers {

    public class SceneConfig {
        public List<Display> displays { get; set; }
        public void handle_incoming_message(object sender, MqttMsgPublishEventArgs msg) { Debug.Log(msg); }
    }

    public class Display {
        public string visualization { get; set; }
        public string reference { get; set; }
        public Dictionary<string, string> details { get; set; }
        public Dictionary<string, object> obj_dic { get; set; }

        public Base container;
        public void construct_container() {
            //Decode dictionary into relevant types
            try { this.obj_dic = Utils.decodeDictionary(this.details); }
            catch { this.obj_dic = new Dictionary<string, object>(); }

            //Identify datatype of required container
            string aqn = typeof(Display).AssemblyQualifiedName.Replace("VRViz.Containers.Display", "VRViz.Containers."+this.visualization);
            Type container_type = Type.GetType(aqn);

            //Attempt to construct the container
            try { this.container = (Base) Activator.CreateInstance(container_type, new object[]{this.reference, this.obj_dic}); }
            catch { Debug.Log(aqn + ": Not found"); }
        }
    }


    public abstract class Base {
        //Define the msg type and ref_type as topic or param
        public abstract string msg_type { get; set; }
        public abstract string reference_type { get; set; }
        public string reference { get; set; }

        public string queue { get; set; }

        //Accept a reference definition from a child class
        public Base(string reference){ this.reference = reference; }

        //Display details of the class
        public void Describe() {
            string msg = "Container defined as type <{0}> pointing to {1} '{2}'";
            Debug.Log(string.Format(msg, this.msg_type, this.reference_type, this.reference));
        }

        //Open the mqtt connection for this display
        public void OpenTopic () {
            Debug.Log("Opening");

        }

        //Apply the message contents to the scene
        public abstract void ApplyMessage();

    }
}