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

public class rviz_default_plugins_PoseWithCovariance : rviz_prefabs.RvizPrefabBase
{

    public rviz_plugins.PoseWithCovariance config_data;
    public geometry_msgs.PoseWithCovarianceStamped message_data;

    public GameObject ArrowAxes;
    public GameObject Covariance;

    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");

        // save message to associated display
        this.config_data = (rviz_plugins.PoseWithCovariance)msg;
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
        Type msgtype = Type.GetType("VRViz.Messages.geometry_msgs.PoseWithCovarianceStamped", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // convert back for validation
        string jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
        Debug.Log("Initial Re-Deserialized JSON object: " + jsonString);

        // save message to associated display
        this.message_data = (geometry_msgs.PoseWithCovarianceStamped)json;
        this.has_new_msg = true;
    }


    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type posewithcovariance");

        // Give configuration details fo the arrow/axes handler
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

        // Give configuration details fo the covariance handler
        covariance_handler handler2 = this.Covariance.GetComponent<covariance_handler>();

        handler2.Position = this.config_data.Covariance.Position;
        handler2.Orientation = this.config_data.Covariance.Orientation;
        
        handler2.SetConfig();
        
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

        // Update positions for Arrow/Axes and Covariance
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
