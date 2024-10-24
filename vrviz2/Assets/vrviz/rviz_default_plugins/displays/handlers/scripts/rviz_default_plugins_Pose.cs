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

public class rviz_default_plugins_Pose : rviz_prefabs.RvizPrefabBase
{

    public rviz_plugins.Pose config_data;
    public geometry_msgs.PoseStamped message_data;

    public GameObject ArrowAxes;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");

        // save message to associated display
        this.config_data = (rviz_plugins.Pose)msg;
        this.has_new_config = true;

        // Subscribe to the associated topic
        Debug.Log(this.initial_config);
        if (this.initial_config == true){
            this.log("initial config it is.");

            // subscribe to topic
            byte[] qos = new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            string[] topic = new string[] { "vrviz"+this.config_data.Topic.Value };
            this.mqtt_client.client.Subscribe(topic, qos);
            
            this.initial_config = false;
        }
    }


    public override void on_topic_message(MqttMsgPublishEventArgs msg) {
        this.log("New data identified.");
        
        // convert byte array to string
        string msgdata = System.Text.Encoding.UTF8.GetString(msg.Message);
        this.log(msgdata);

        // convert string to json object
        Type msgtype = Type.GetType("VRViz.Messages.geometry_msgs.PoseStamped", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // convert back for validation
        string jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
        Debug.Log("Initial Re-Deserialized JSON object: " + jsonString);

        // save message to associated display
        this.message_data = (geometry_msgs.PoseStamped)json;
        this.has_new_msg = true;
    }

    // Respond to recieved message
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type pose");

        arrowaxes_handler handler = this.ArrowAxes.GetComponent<arrowaxes_handler>();

        handler.Alpha = this.config_data.Alpha;
        handler.AxesLength = this.config_data.AxesLength;
        handler.AxesRadius = this.config_data.AxesRadius;
        handler.Color = this.config_data.Color;
        handler.HeadLength = this.config_data.HeadLength;
        handler.HeadRadius = this.config_data.HeadRadius;
        handler.ShaftLength = this.config_data.ShaftLength;
        handler.ShaftRadius = this.config_data.ShaftRadius;
		handler.Shape = this.config_data.Shape;
        
        handler.SetConfig();
    }
    
    // Resond to recieved message
    public override void apply_new_msg() {
        this.has_new_msg = false;

        string jsonString = JsonConvert.SerializeObject(this.message_data, Formatting.Indented);
        Debug.Log("Re-Deserialized JSON object: " + jsonString);

        if (this.message_data == null){
            Debug.LogError("this.message_data is null");
            return;
        }

        //move arrow to new position and orientation
        this.ArrowAxes.transform.localPosition = new Vector3(
            (float)this.message_data.pose.position.x.data, 
            (float)this.message_data.pose.position.z.data, 
            (float)this.message_data.pose.position.y.data);

        Quaternion inputQuaternion = new Quaternion(
            (float)this.message_data.pose.orientation.x.data, 
            (float)this.message_data.pose.orientation.y.data, 
            (float)this.message_data.pose.orientation.z.data, 
            (float)this.message_data.pose.orientation.w.data);
        Vector3 euler = inputQuaternion.eulerAngles;
        euler.x = 0;
        euler.y = -euler.z;
        euler.z = 0;
        this.ArrowAxes.transform.localRotation = Quaternion.Euler(euler);
        
        this.message_data = null;
    }
}
