using UnityEngine;
using UnityEngine.UI;
using VRViz.Connections;
using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using geometry_msgs = VRViz.Messages.geometry_msgs;

namespace VRViz.plugins.rviz_default_plugins.prefabs {

    public abstract class RvizPrefabBase : MonoBehaviour
    {
        // Generic items required within the Pipeline class should be defined here.
        public ClientManager mqtt_client;
        public Text text_log;
        public bool has_new_config;
        public bool has_new_msg;
        public bool initial_config;

        void Update()
        {

            if (this.has_new_config){
                this.apply_new_config();
                this.has_new_config = false;
            }    
            if (this.has_new_msg){
                this.apply_new_msg();
                this.has_new_msg = false;
            }

        }

        
        public abstract void on_config_message(rviz_general.Display msg);
        public abstract void on_topic_message(MqttMsgPublishEventArgs msg);

        public abstract void apply_new_config();
        public abstract void apply_new_msg();

        public void log(string txt) {
            Debug.Log(txt);
            this.text_log.text = txt;
        }

    }

}