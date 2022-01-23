using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using VRViz.Containers;


namespace VRViz.Setup {
    public class NMap : MonoBehaviour
    {
        public string url;

        // Start is called before the first frame update
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
            SceneConfig config = ParseConfig(contents);

            //construct the rviz_containers for each display
            foreach( VRViz.Containers.Display display in config.displays ) {
                display.construct_container();
                //display.container.Describe();
            }

            //open topics
            foreach( VRViz.Containers.Display display in config.displays ) {
                display.container.OpenTopic();
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

    }
}