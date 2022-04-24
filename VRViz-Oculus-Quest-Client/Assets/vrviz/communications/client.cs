using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using VRViz.Containers;

namespace VRViz.Connections {
    public class ClientManager {
        string ip, mqttUserName, mqttPassword, clientId = Guid.NewGuid().ToString();
        int port;
        public bool on_connection_action = false;
        SceneConfig config;
        public MqttClient client;

        public ClientManager(string ip, int port, SceneConfig config, string mqttUserName, string mqttPassword) {
            Profiler.BeginSample("VRViz.Connections::ClientManager.ClientManager");
            this.ip = ip;
            this.port = port;
            this.mqttUserName = mqttUserName;
            this.mqttPassword = mqttPassword;
            this.config = config;
            Profiler.EndSample();
        }

        public IEnumerator Connect() { //yield return new WaitForSecondsRealtime(0.5f);
            Profiler.BeginSample("VRViz.Connections::ClientManager.Connect");
            this.client = new MqttClient(this.ip, this.port, false, null, null, MqttSslProtocols.None);
            this.client.MqttMsgPublishReceived += this.config.handle_incoming_message;
            try {
                this.client.Connect(this.clientId, this.mqttUserName, this.mqttPassword);
                this.on_connection_action = true;
                Debug.Log("Connection result: success");
            } catch { Debug.Log("Connection result: failure."); }
            Profiler.EndSample();
            yield return null;
        }
        public void Disconnect() { try { this.client.Disconnect(); } finally { this.client = null; } }
    }
}