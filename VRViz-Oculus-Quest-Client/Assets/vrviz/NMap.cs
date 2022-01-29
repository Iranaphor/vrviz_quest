using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using VRViz.Containers;
using VRViz.Connections;


namespace VRViz.Setup {
    public class NMap : MonoBehaviour
    {
        public string url;
        public SceneConfig config;
        private ClientManager client;

        void Start()
        {
            //start unity-transport
            //    string ip = this.SCAN_FOR_IP()

            //download file list
            //    selected = "url/"+filelist[0]
            //    if len(filelist) > 1:
            //        selection = input(filelist)
            //        selected = "url/"+selection

            //download selected file
            string contents;
            using (var wc = new System.Net.WebClient())
                contents = wc.DownloadString(url);
            Debug.Log(contents);

            //parse file
            this.config = ParseConfig(contents);

            //construct the rviz_containers for each display
            foreach( VRViz.Containers.Display display in this.config.displays ) {
                display.construct_container();
                //display.container.Describe();
            }

            //initiate mqtt
            this.client = new ClientManager("127.0.0.1", 7781, this.config, null, null);
            StartCoroutine(this.client.Connect());
        }

        void Update() {
            //Debug.Log("Client-Specs " + this.client.client.IsConnected + " " + this.client.on_connection_action);
            if (this.client.client.IsConnected) {
                if (this.client.on_connection_action) {
                    foreach( VRViz.Containers.Display d in this.config.displays ) d.container.OpenTopic(this.client.client);
                    client.on_connection_action = false;
                }
            }
        }

        public static SceneConfig ParseConfig(string ymlContents) {
            //Parse the input `yaml` into an object `SceneConfig`, using the Underscored naming convention
            UnderscoredNamingConvention conv = new UnderscoredNamingConvention();
            var deserializer = new DeserializerBuilder().WithNamingConvention(conv).Build();
            return deserializer.Deserialize<SceneConfig>(ymlContents);
        }

        public static string SCAN_FOR_IP() {
            Debug.Log("Scanning for IP...");
            return "ip";
        }

        protected virtual void OnApplicationQuit() { client.Disconnect(); }

    }
}