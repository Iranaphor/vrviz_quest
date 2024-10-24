using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using VRViz.Connections;

// 1. CHANGE TO APPROPRIATE DATA MSG GROUP
using std_msgs = VRViz.Messages.std_msgs;
using sensor_msgs = VRViz.Messages.sensor_msgs;

using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using rviz_plugins = VRViz.plugins.rviz_default_plugins.plugins;
using rviz_prefabs = VRViz.plugins.rviz_default_plugins.prefabs;

// 2. CHANGE NAME TO END WITH APPROPRIATE PLUGIN MSG TYPE
public class rviz_default_plugins_LaserScan : rviz_prefabs.RvizPrefabBase
{

    // 3. CHANGE TO APPROPRIATE PLUGIN MSG TYPE
    public rviz_plugins.LaserScan config_data;

    // 4. CHANGE TO APPROPRIATE DATA MSG TYPE
    public sensor_msgs.LaserScan message_data;

    // 5. ADD ANY GAMEOBJECTS TO INTERACT WITH HERE (THEY WILL BE ATTACHED TO THIS AS A PREFAB)
    public GameObject LaserScanHandler;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");


        // 6. SET THE CAST TYPE TO THE APPROPRIATE PLUGIN MSG TYPE
        // save message to associated display
        this.config_data = (rviz_plugins.LaserScan)msg; 
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

        // 7. SET THE CAST TYPE TO THE APPROPRIATE DATA MSG TYPE
        // convert string to json object
        Type msgtype = Type.GetType("VRViz.Messages.sensor_msgs.LaserScan", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // convert back for validation
        // string jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
        // Debug.Log("Initial Re-Deserialized JSON object: " + jsonString);

        // 8. SET THE CAST TYPE TO THE APPROPRIATE DATA MSG TYPE
        // save message to associated display
        this.message_data = (sensor_msgs.LaserScan)json;
        this.has_new_msg = true;
    }

    // Respond to recieved config
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type DEFAULT");

        // 9. GET GAMEOBJECT
        point_handler handler = this.LaserScanHandler.GetComponent<point_handler>();

        // 10. APPLY PROPERTIES
        handler.Style = this.config_data.Style as string;
        handler.Alpha = Convert.ToSingle(this.config_data.Alpha);
        handler.ColorStr = this.config_data.Color as string;
        handler.SizeMeters = Convert.ToSingle(this.config_data.SizeMeters);
        // handler.SizeMeters = 1;
        handler.DecayTime = Convert.ToSingle(this.config_data.DecayTime);
        handler.UseRainbow = Convert.ToBoolean(this.config_data.UseRainbow);
        handler.InvertRainbow = Convert.ToBoolean(this.config_data.InvertRainbow);
        handler.MinIntensity = Convert.ToSingle(this.config_data.MinIntensity);
        handler.MaxIntensity = Convert.ToSingle(this.config_data.MaxIntensity);
        handler.Axis = this.config_data.Axis as string;
        handler.ChannelName = this.config_data.ChannelName as string;

        // 11. SAVE PROPERTIES
        handler.SetConfig();    

    }

    // Resond to recieved message
    public override void apply_new_msg() {
        this.has_new_msg = false;
        this.set_frame(this.message_data.header.frame_id.data);

        if (this.message_data == null)
        {
            Debug.LogError("this.message_data is null");
            return;
        }

        // Get the point_handler component
        point_handler handler = this.LaserScanHandler.GetComponent<point_handler>();

        float angle = this.message_data.angle_min.data;

        // Get the handler's global scale
        Vector3 handlerScale = handler.transform.lossyScale;

        // Avoid division by zero
        if (handlerScale.x == 0) handlerScale.x = 1;
        if (handlerScale.y == 0) handlerScale.y = 1;
        if (handlerScale.z == 0) handlerScale.z = 1;

        // Update existing points or create new ones as needed
        int pointIndex = 0;

        for (int i = 0; i < this.message_data.ranges.Length; i++)
        {
            float range = this.message_data.ranges[i].data;
            float intensity = (this.message_data.intensities != null && this.message_data.intensities.Length > i) ? this.message_data.intensities[i].data : 0f;

            // Skip invalid measurements
            if (range < this.message_data.range_min.data || range > this.message_data.range_max.data)
            {
                angle += this.message_data.angle_increment.data;
                continue;
            }

            // Calculate position
            Vector3 position = handler.CalculatePosition(range, angle);
            position = new Vector3(
                position.x / handlerScale.x,
                position.y / handlerScale.y,
                position.z / handlerScale.z
            );
            GameObject point;

            if (pointIndex < handler.scanPoints.Count)
            {
                // Reuse existing point
                point = handler.scanPoints[pointIndex];
                point.SetActive(true);
            }
            else
            {
                // Instantiate new point and add to list
                point = Instantiate(handler.pointsPrefab, handler.transform);
                handler.scanPoints.Add(point);
                handler.pointTimestamps.Add(Time.time); // Record spawn time for decay
            }

            // Update point properties
            point.transform.localPosition = position;
            point.transform.localScale = Vector3.one * handler.SizeMeters;

            handler.ApplyVisualProperties(point, intensity);

            // Update timestamp for decay
            handler.pointTimestamps[pointIndex] = Time.time;

            pointIndex++;
            angle += this.message_data.angle_increment.data;
        }

        // Deactivate any excess points
        for (int i = pointIndex; i < handler.scanPoints.Count; i++)
        {
            handler.scanPoints[i].SetActive(false);
        }

        this.message_data = null;
    }

}
