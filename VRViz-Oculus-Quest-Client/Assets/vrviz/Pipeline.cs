using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;
using VRViz.Containers;
using VRViz.Connections;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VRViz.Pipeline {
    public class Pipeline : MonoBehaviour
    {
        //private string url = "https://raw.githubusercontent.com/Iranaphor/VRViz_ROS/main/config/vrviz_4_display_scene_config.yaml?token=GHSAT0AAAAAABL7KLGDZPS2AWINNF2YFAIUYP6W7GA";
        public SceneConfig config;
        private ClientManager client;
        private int LAN_PORT = 8608;

        private string mqtt_ip = "127.0.0.1";
        private int mqtt_port = 7781;
        private int web_server_port = 8080;

        void Start()
        {
            Profiler.BeginSample("VRViz.Pipeline::Pipeline.Start");
            Discover();

            string url1 = "http://" + mqtt_ip + ":" + web_server_port.ToString() + "/vrviz_4_display_scene_config.yaml";
            string contents;
            using (var wc = new System.Net.WebClient())
                contents = wc.DownloadString(url1);
            Debug.Log(contents);

            //parse file
            this.config = ParseConfig(contents);

            //construct the rviz_containers for each display
            foreach( VRViz.Containers.Display display in this.config.displays ) {
                display.construct_container();
            }
            this.config.convert_displays_to_dict();

            //initiate mqtt
            this.client = new ClientManager(this.mqtt_ip, this.mqtt_port, this.config, null, null);
            StartCoroutine(this.client.Connect());

            Profiler.EndSample();
        }

        void Update() {
            Profiler.BeginSample("VRViz.Pipeline::Pipeline.Update");
            if (this.client.client.IsConnected) {

                //once connected, open a topic for each container
                if (this.client.on_connection_action) {
                    foreach(KeyValuePair<string, VRViz.Containers.Display> item in this.config.displays_dictionary){
                        item.Value.container.OpenTopic(this.client.client);
                    }
                    client.on_connection_action = false;
                }

                //for each container, apply message if one exists
                foreach(KeyValuePair<string, VRViz.Containers.Display> item in this.config.displays_dictionary){
                    item.Value.container.ApplyIfMessage();
                }

            }
            Profiler.EndSample();
        }

        public static SceneConfig ParseConfig(string ymlContents) {
            Profiler.BeginSample("VRViz.Pipeline::Pipeline.ParseConfig");
            //Parse the input `yaml` into an object `SceneConfig`, using the Underscored naming convention
            UnderscoredNamingConvention conv = new UnderscoredNamingConvention();
            var deserializer = new DeserializerBuilder().WithNamingConvention(conv).Build();
            Profiler.EndSample();
            return deserializer.Deserialize<SceneConfig>(ymlContents);
        }


        protected virtual void OnApplicationQuit() { client.Disconnect(); }

        public void Discover(){
            Profiler.BeginSample("VRViz.Pipeline::Pipeline.Discover");

			byte[] data = null;
            UdpClient udp_client = new UdpClient(LAN_PORT);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, LAN_PORT);
            try{
                data = udp_client.Receive(ref ep);
                Debug.Log(System.Text.Encoding.UTF8.GetString(data));
            } catch( Exception e ) { Debug.Log(e); }
            finally { udp_client.Close(); }

            string[] values = System.Text.Encoding.UTF8.GetString(data).Split('-');
            this.web_server_port = int.Parse(values[3]);
            this.mqtt_ip = values[1];
            this.mqtt_port = int.Parse(values[2]);

            Profiler.EndSample();
		}


    }
}
