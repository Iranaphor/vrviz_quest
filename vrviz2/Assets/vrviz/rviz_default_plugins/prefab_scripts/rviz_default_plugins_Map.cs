using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using VRViz.Connections;
using std_msgs = VRViz.Messages.std_msgs;
using geometry_msgs = VRViz.Messages.geometry_msgs;
using nav_msgs = VRViz.Messages.nav_msgs;
//using nav_msgs = VRViz.interfaces.nav_msgs.msgs;

using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using rviz_plugins = VRViz.plugins.rviz_default_plugins.plugins;
using rviz_prefabs = VRViz.plugins.rviz_default_plugins.prefabs;

public class rviz_default_plugins_Map : rviz_prefabs.RvizPrefabBase
{

    public rviz_plugins.Map config_data;
    public nav_msgs.OccupancyGrid message_data;

    public GameObject ImagePlane;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");

        // save message to associated display
        this.config_data = (rviz_plugins.Map)msg;
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
        Type msgtype = Type.GetType("VRViz.Messages.nav_msgs.OccupancyGrid", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // convert back for validation
        string jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
        Debug.Log("Initial Re-Deserialized JSON object: " + jsonString);

        // save message to associated display
        this.message_data = (nav_msgs.OccupancyGrid)json;
        this.has_new_msg = true;
    }

    // Respond to recieved message
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type Map");

        // arrowaxes_handler handler = this.ArrowAxes.GetComponent<arrowaxes_handler>();

        // handler.Alpha = this.config_data.Shape.Alpha;
        // handler.AxesLength = this.config_data.Shape.AxesLength;
        // handler.AxesRadius = this.config_data.Shape.AxesRadius;
        // handler.Color = this.config_data.Shape.Color;
        // handler.HeadLength = this.config_data.Shape.HeadLength;
        // handler.HeadRadius = this.config_data.Shape.HeadRadius;
        // handler.ShaftLength = this.config_data.Shape.ShaftLength;
        // handler.ShaftRadius = this.config_data.Shape.ShaftRadius;
		// handler.Shape = this.config_data.Shape.Value;
        
        // handler.SetConfig();
        
        // Give configuration details fo the covariance handler
        // covariance_handler handler2 = this.Covariance.GetComponent<covariance_handler>();

        // handler2.Position = this.config_data.Covariance.Position;
        // handler2.Orientation = this.config_data.Covariance.Orientation;
        
        // handler2.SetConfig();
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

        // Extract map info
        uint width = this.message_data.info.width.data;
        uint height = this.message_data.info.height.data;
        float resolution = this.message_data.info.resolution.data;
        geometry_msgs.Pose origin = this.message_data.info.origin;
        std_msgs.Int8[] data = this.message_data.data;

        // Prepare the plane
        float mapWidth = width * resolution;
        float mapHeight = height * resolution;
        this.ImagePlane.transform.localScale = new Vector3(mapWidth / 10f, 1, mapHeight / 10f);

        // Create texture
        Texture2D texture = new Texture2D((int)width, (int)height, TextureFormat.RGB24, false);
        Color[] pixels = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                int index = x + y * (int)width;
                sbyte value = data[index].data;
                Color color;
                switch (value)
                {
                    case -1:
                        color = Color.gray; // Unknown
                        break;
                    case 0:
                        color = Color.white; // Free space
                        break;
                    default:
                        color = Color.black; // Occupied
                        break;
                }
                pixels[index] = color;
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        // Apply texture
        this.ImagePlane.GetComponent<Renderer>().material.mainTexture = texture;

        // Position the plane
        this.ImagePlane.transform.position = new Vector3(
            (float)origin.position.x.data + mapWidth / 2,
            (float)origin.position.y.data,
            (float)origin.position.z.data + mapHeight / 2
        );

        // Rotate the plane
        Quaternion rotation = new Quaternion(
            (float)origin.orientation.x.data,
            (float)origin.orientation.y.data,
            (float)origin.orientation.z.data,
            (float)origin.orientation.w.data
        );
        this.ImagePlane.transform.rotation = rotation;

        this.message_data = null;
    }
}
