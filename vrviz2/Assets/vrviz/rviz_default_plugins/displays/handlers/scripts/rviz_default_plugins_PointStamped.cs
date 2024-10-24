using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using VRViz.Connections;
using geometry_msgs = VRViz.Messages.geometry_msgs;
//using geometry_msgs = VRViz.interfaces.geometry_msgs.msgs;

using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using rviz_plugins = VRViz.plugins.rviz_default_plugins.plugins;
using rviz_prefabs = VRViz.plugins.rviz_default_plugins.prefabs;

public class rviz_default_plugins_PointStamped : rviz_prefabs.RvizPrefabBase
{

    public rviz_plugins.PointStamped config_data;
    public geometry_msgs.PointStamped message_data;
    public GameObject PointBall;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");

        // save message to associated display
        this.config_data = (rviz_plugins.PointStamped)msg;
        this.has_new_config = true;

        // Subscribe to the associated topic
        Debug.Log(this.initial_config);
        if (this.initial_config == true){
            this.log("initial config it is.");

            // subscribe to topic
            byte[] qos = new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            string[] topic = new string[] { this.mqtt_namespace+"/TOPIC"+this.config_data.Topic.Value };
            this.mqtt_client.client.Subscribe(topic, qos);
            
            this.initial_config = false;
        }
    }
    
    public override void on_topic_message(MqttMsgPublishEventArgs msg) {
        this.log("New data identified.");

        // convert string to json object
        string msgdata = System.Text.Encoding.UTF8.GetString(msg.Message);
        Type msgtype = Type.GetType("VRViz.Messages.geometry_msgs.PointStamped", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // save message to associated display
        this.message_data = (geometry_msgs.PointStamped)json;
        this.has_new_msg = true;
    }


    // Resond to recieved message
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type point");

        pointball_handler handler = this.PointBall.GetComponent<pointball_handler>();

		handler.Alpha = this.config_data.Alpha;
		handler.Color = this.config_data.Color;
		handler.Radius = this.config_data.Radius;
		handler.HistoryLength = this.config_data.HistoryLength;
        
        handler.SetConfig();
    }




    
    // Resond to recieved message
    public override void apply_new_msg() {
        this.log("new msg being applied of type point");
        this.has_new_msg = false;
        this.set_frame(this.message_data.header.frame_id.data);
        
        //string jsonString = JsonConvert.SerializeObject(this.message_data, Formatting.Indented);
        //Debug.Log("Re-Deserialized JSON object: " + jsonString);

        if (this.message_data == null){
            Debug.LogError("this.message_data.position is null");
            return;
        }
        
        //move ball to new position
        this.PointBall.transform.localPosition = new Vector3(
            (float)this.message_data.point.x.data, 
            (float)this.message_data.point.z.data, 
            (float)this.message_data.point.y.data);

        this.message_data = null;

    }


}

// namespace VRViz.plugins.rviz_default_plugins.plugins {
// 	public class Pose {
// 		public float Alpha;
// 		public float AxesLength;
// 		public float AxesRadius;
// 		public string Class;
// 		public rviz_utils::Color Color;
// 		public bool Enabled;
// 		public float HeadLength;
// 		public float HeadRadius;
// 		public string Name;
// 		public float ShaftLength;
// 		public float ShaftRadius;
// 		public string Shape;
// 		public rviz_utils::Topic Topic;
// 		public bool Value;
//     }
// }
