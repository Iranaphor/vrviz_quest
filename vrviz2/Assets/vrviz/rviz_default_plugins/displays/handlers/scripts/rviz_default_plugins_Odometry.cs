using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using VRViz.Connections;
using nav_msgs = VRViz.Messages.nav_msgs;
//using nav_msgs = VRViz.interfaces.nav_msgs.msgs;

using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using rviz_plugins = VRViz.plugins.rviz_default_plugins.plugins;
using rviz_prefabs = VRViz.plugins.rviz_default_plugins.prefabs;

public class rviz_default_plugins_Odometry : rviz_prefabs.RvizPrefabBase
{

    public rviz_plugins.Odometry config_data;
    public nav_msgs.Odometry message_data;

    public GameObject ArrowAxes;
    public GameObject Covariance;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");

        // save message to associated display
        this.config_data = (rviz_plugins.Odometry)msg;
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
        
        // convert byte array to string
        string msgdata = System.Text.Encoding.UTF8.GetString(msg.Message);
        this.log(msgdata);

        // convert string to json object
        Type msgtype = Type.GetType("VRViz.Messages.nav_msgs.Odometry", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // convert back for validation
        string jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
        Debug.Log("Initial Re-Deserialized JSON object: " + jsonString);

        // save message to associated display
        this.message_data = (nav_msgs.Odometry)json;
        this.has_new_msg = true;
    }

    // Respond to recieved message
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type odometry");

        arrowaxes_handler handler = this.ArrowAxes.GetComponent<arrowaxes_handler>();

        handler.Alpha = this.config_data.Shape.Alpha;
        handler.AxesLength = this.config_data.Shape.AxesLength;
        handler.AxesRadius = this.config_data.Shape.AxesRadius;
        handler.Color = this.config_data.Shape.Color;
        handler.HeadLength = this.config_data.Shape.HeadLength;
        handler.HeadRadius = this.config_data.Shape.HeadRadius;
        handler.ShaftLength = this.config_data.Shape.ShaftLength;
        handler.ShaftRadius = this.config_data.Shape.ShaftRadius;
		handler.Shape = this.config_data.Shape.Value;
        
        handler.SetConfig();
        
        // Give configuration details fo the covariance handler
        covariance_handler handler2 = this.Covariance.GetComponent<covariance_handler>();

        handler2.Position = this.config_data.Covariance.Position;
        handler2.Orientation = this.config_data.Covariance.Orientation;
        
        handler2.SetConfig();
    }
    
    // Resond to recieved message
    public override void apply_new_msg() {
        this.has_new_msg = false;
        this.set_frame(this.message_data.header.frame_id.data);

        string jsonString = JsonConvert.SerializeObject(this.message_data, Formatting.Indented);
        Debug.Log("Re-Deserialized JSON object: " + jsonString);

        if (this.message_data == null){
            Debug.LogError("this.message_data is null");
            return;
        }

        //move arrow to new position and orientation
        this.ArrowAxes.transform.localPosition = new Vector3(
            (float)this.message_data.pose.pose.position.x.data, 
            (float)this.message_data.pose.pose.position.z.data, 
            (float)this.message_data.pose.pose.position.y.data);
        this.Covariance.transform.localPosition = new Vector3(
            (float)this.message_data.pose.pose.position.x.data, 
            (float)this.message_data.pose.pose.position.z.data, 
            (float)this.message_data.pose.pose.position.y.data);

        // Update Arrow/Axes orientation
        Quaternion inputQuaternion = new Quaternion(
            (float)this.message_data.pose.pose.orientation.x.data, 
            (float)this.message_data.pose.pose.orientation.y.data, 
            (float)this.message_data.pose.pose.orientation.z.data, 
            (float)this.message_data.pose.pose.orientation.w.data);
        Vector3 euler;

        euler = inputQuaternion.eulerAngles;
        euler.x = 0.0f;
        euler.y = -euler.z;
        euler.z = 0.0f;
        this.ArrowAxes.transform.localRotation = Quaternion.Euler(euler);

        euler = inputQuaternion.eulerAngles;
        euler.x = 0.0f;
        euler.y = -euler.z;
        euler.z = 0.0f;
        this.Covariance.transform.localRotation = Quaternion.Euler(euler);
        
        // Update size of covariance bubble
        // float scale;
        // scale = handler.Position.Scale;
        // handler.PositionCylinder.transform.localScale = new Vector3(
        //     (float)this.message_data.pose.pose.position.x.data * scale, 
        //     (float)0.01f, 
        //     (float)this.message_data.pose.pose.position.y.data * scale);
        // scale = handler.Orientation.Scale;
        // handler.OrientationCone.transform.localScale = new Vector3(
        //     (float)this.message_data.pose.pose.position.x.data * scale, (float)0.5f, (float)0.01f);

        this.message_data = null;
    }
}
