using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Profiling;
using VRViz.Containers;
using VRViz.Connections;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using rviz_general = VRViz.plugins.rviz_default_plugins.general;
using rviz_prefabs = VRViz.plugins.rviz_default_plugins.prefabs;

namespace VRViz.Pipeline {
    public class Pipeline : MonoBehaviour {

        public SceneConfig config;
        private ClientManager client;

        private bool skip_input = true;
        // private string default_mqtt_ip = "127.0.0.1";
        public string default_mqtt_ip = "192.168.137.95";
        public int default_mqtt_port = 8883;

        private Dictionary<string, GameObject> displays = new Dictionary<string, GameObject>();
        private List<rviz_general.Display> queue_prefab_generation;
        private List<MqttMsgPublishEventArgs> queue_prefab_msg = new List<MqttMsgPublishEventArgs>();

        public Text text_log;
        public GameObject rviz_table;

        void Start()
        {
            if (this.skip_input == true) {
                //hide mqtt input boxes
                // mqtt_ip_input.getgameobject().disable = True
                // mqtt_port_input.getgameobject().disable = True

                //connect to mqtt
                connect_to_mqtt(this.default_mqtt_ip, this.default_mqtt_port);
            }
        }
        public void on_connect_button_click()
        {
            // string mqtt_ip = mqtt_ip_input.getgameobject().text
            // string mqtt_port = mqtt_port_input.getgameobject().text
            // connect_to_mqtt(mqtt_ip, mqtt_port);
        }
        public void connect_to_mqtt(string mqtt_ip, int mqtt_port)
        {
            //initiate mqtt
            this.client = new ClientManager(mqtt_ip, mqtt_port, null, null);
            StartCoroutine(this.client.Connect());
        }


        void Update() {
            if (this.client == null) {
                Debug.LogError("Client is null...");
                return;
            }
            if (this.client.client.IsConnected) {

                //once connected, open a topic for the configuration. details
                if (this.client.on_connection_action) {
                    // define on_message callback
                    this.client.client.MqttMsgPublishReceived += this.on_message;
                    this.client.on_connection_action = false;

                    this.text_log.text = "connection completed wooo";
                    // this.client.client.Publish("vrviz/LOG", "connection completed wooo");
                    //Debug.Log("connection completed woo");

                    // subscribe to topic
                    byte[] qos = new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
                    string[] topic = new string[] { "vrviz/META" };
                    this.client.client.Subscribe(topic, qos);
                }

                // if any prefabs need to be instantiated do that here
                // populated from the on_message method recieving a message from the META topic
                if (this.queue_prefab_generation != null) {
                    List<rviz_general.Display> queue = this.queue_prefab_generation;
                    this.queue_prefab_generation = null;

                    foreach (rviz_general.Display display in queue) {  
                        Debug.Log("Setting up Prefab for Topic: "+display.Topic.Value);  

                        // create new prefab
                        string classType = display.Class.Substring(display.Class.IndexOf('/') + 1);
                        string prefabPath = "prefabs/rviz_default_plugins_" + classType;
                        Debug.Log("Setting up Prefab of Type: " + prefabPath);
                        GameObject prefab = Resources.Load<GameObject>(prefabPath);

                        if (prefab != null) {
                            GameObject go = Instantiate(prefab);
                            go.transform.SetParent(rviz_table.transform, false);
                            
                            // save prefab for later use
                            this.displays["vrviz"+display.Topic.Value] = go;
                            go.GetComponent<rviz_prefabs.RvizPrefabBase>().initial_config = true;
                            go.GetComponent<rviz_prefabs.RvizPrefabBase>().text_log = this.text_log;
                            go.GetComponent<rviz_prefabs.RvizPrefabBase>().mqtt_client = this.client;
                            go.GetComponent<rviz_prefabs.RvizPrefabBase>().on_config_message(display);

                        } else {
                            Debug.LogError("Prefab not found at path: " + prefabPath);
                        }

                    }
                    
                }

                // Apply any messages that came in through async process
                if (this.queue_prefab_msg != null) {
                    List<MqttMsgPublishEventArgs> queue = this.queue_prefab_msg;
                    this.queue_prefab_msg = new List<MqttMsgPublishEventArgs>();

                    foreach (MqttMsgPublishEventArgs msg in queue) {
                        Debug.Log("Setting up Prefab for Topic: "+msg.Topic);
                        GameObject go = (GameObject)this.displays[msg.Topic];
                        go.GetComponent<rviz_prefabs.RvizPrefabBase>().on_topic_message(msg);
                    }
                    
                }

            } else {
                this.text_log.text = "connection failed now";
            }
        }

        public void on_message(object sender, MqttMsgPublishEventArgs raw_msg) {

            // convert message to string
            string msg = System.Text.Encoding.UTF8.GetString(raw_msg.Message);
            Debug.Log("JSON string: " + msg);

            // if the message is detailing a new configuration
            if (raw_msg.Topic == "vrviz/META") {
                //Debug.Log("Recieved META from vrviz/META");

                // convert string to json object
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new DisplayConverter());
                var json = JsonConvert.DeserializeObject<rviz_general.Config>(msg, settings);
              
                // Serialize the json object back to a string and print it
                //string jsonString = JsonConvert.SerializeObject(json, Formatting.Indented);
                //Debug.Log("Re-Deserialized JSON object: " + jsonString);

                //check if null
                if (json == null) {
                    Debug.LogError("Deserialized json is null. Check the JSON string and Config class.");
                    return; // Exit the method to avoid further null reference exceptions
                }
                if (json.VisualizationManager == null) {
                    Debug.LogError("VisualizationManager is null. Check the JSON string for VisualizationManager data.");
                    return; // Exit the method
                }
                if (json.VisualizationManager.Displays == null) {
                    Debug.LogError("Displays is null. Check the JSON string for Displays data.");
                    return; // Exit the method
                }

                // Loop through each display listed in the configuration
                this.queue_prefab_generation = new List<rviz_general.Display>();
                foreach (rviz_general.Display display in json.VisualizationManager.Displays) {
       
                    //check if null
                    if (display == null) {
                        Debug.LogError("Display is null.");
                        return; // Exit the method
                    }
                    if (display.Name == null) {
                        Debug.LogError("Display.Name is null.");
                        return; // Exit the method
                    }
                    if (display.Topic == null) {
                        Debug.LogError("Display.Topic is null.");
                        return; // Exit the method
                    }
                    if (display.Topic.Value == null) {
                        Debug.LogError("Display.Topic.Value is null.");
                        return; // Exit the method
                    }

                    // Either create a new prefab, or update the status of an existing one
                    GameObject go = null;
                    if (this.displays.ContainsKey(display.Topic.Value)) {
                        // get existing reference
                        Debug.Log("Updating config for Topic: "+display.Topic.Value);
                        go = (GameObject)this.displays["vrviz"+display.Topic.Value];
                        go.GetComponent<rviz_prefabs.RvizPrefabBase>().on_config_message(display);
                    } else {
                        this.queue_prefab_generation.Add(display);
                    }
                }
            

            } else {
                // ... or send message to prefab for processing
                Debug.Log("Updating data for Topic: "+raw_msg.Topic);
                this.queue_prefab_msg.Add(raw_msg);
            }
        }
    }
}




public class DisplayConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(rviz_general.Display)); //only execute if is of type rviz_general.Display
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        string originalString = jsonObject["Class"].Value<string>();
        int indexOfPeriod = originalString.IndexOf('/');
        string classType = originalString.Substring(indexOfPeriod + 1);
        
        //Debug.Log("Custom Json Load for: VRViz.plugins.rviz_default_plugins.plugins." + classType);
        Type displayType = Type.GetType("VRViz.plugins.rviz_default_plugins.plugins." + classType);

        if (displayType == null)
            throw new JsonSerializationException("Unknown class type: " + classType);

        object obj = JsonConvert.DeserializeObject(jsonObject.ToString(), displayType);

        // Reseralise the obj to check if it has the data we need
        //string jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented);
        //Debug.Log("Re-Deserialized JSON object: " + jsonString);

        return obj;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
