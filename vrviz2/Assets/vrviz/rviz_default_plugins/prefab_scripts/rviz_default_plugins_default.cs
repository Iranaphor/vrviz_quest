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

using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using rviz_plugins = VRViz.plugins.rviz_default_plugins.plugins;
using rviz_prefabs = VRViz.plugins.rviz_default_plugins.prefabs;

// 2. CHANGE NAME TO END WITH APPROPRIATE PLUGIN MSG TYPE
public class rviz_default_plugins_DEFAULT : rviz_prefabs.RvizPrefabBase
{

    // 3. CHANGE TO APPROPRIATE PLUGIN MSG TYPE
    public rviz_plugins.DEFAULT config_data;

    // 4. CHANGE TO APPROPRIATE DATA MSG TYPE
    public std_msgs.Empty message_data;

    // 5. ADD ANY GAMEOBJECTS TO INTERACT WITH HERE (THEY WILL BE ATTACHED TO THIS AS A PREFAB)
    // public GameObject object_reference;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");


        // 6. SET THE CAST TYPE TO THE APPROPRIATE PLUGIN MSG TYPE
        // save message to associated display
        this.config_data = (rviz_plugins.DEFAULT)msg; 
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

        // 7. SET THE CAST TYPE TO THE APPROPRIATE DATA MSG TYPE
        // convert string to json object
        Type msgtype = Type.GetType("VRViz.Messages.std_msgs.Empty", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);

        // convert back for validation
        string jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
        Debug.Log("Initial Re-Deserialized JSON object: " + jsonString);

        // 8. SET THE CAST TYPE TO THE APPROPRIATE DATA MSG TYPE
        // save message to associated display
        this.message_data = (std_msgs.Empty)json;
        this.has_new_msg = true;
    }

    // Respond to recieved message
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type DEFAULT");

        // 9. GET GAMEOBJECT
        // gameobj_handler handler = this.object_reference.GetComponent<gameobj_handler>();

        // 10. APPLY PROPERTIES
        // handler.property = this.config_data.property
        
        // 11. SAVE PROPERTIES
        // handler.SetConfig();
        

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

        // 12. APPLY TOPIC DATA TO THE RELEVANT GAMEOBJECTS
        // this.gameobject_reference = data
        

        this.message_data = null;
    }
}
