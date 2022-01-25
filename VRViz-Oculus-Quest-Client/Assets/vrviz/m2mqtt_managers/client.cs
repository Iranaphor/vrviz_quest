using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json;
using VRViz.Containers;

namespace VRViz.Connections {
    public class ClientManager {
        string ip, mqttUserName, mqttPassword, clientId = Guid.NewGuid().ToString();
        int port;
        SceneConfig config;
        MqttClient client;

        public ClientManager(string ip, int port, SceneConfig config, string mqttUserName, string mqttPassword) {
            this.ip = ip;
            this.port = port;
            this.mqttUserName = mqttUserName;
            this.mqttPassword = mqttPassword;
            this.config = config;
            Debug.Log("Constructor Complete");
        }

        public IEnumerator Connect() {
            Debug.Log("Connect() Begun");
            try {
                this.client = new MqttClient(this.ip, this.port, false, null, null, MqttSslProtocols.None);
                this.client.Settings.TimeoutOnConnection = 5; //MqttSettings.MQTT_CONNECT_TIMEOUT;
                this.client.Settings.TimeoutOnReceiving = 5; //MqttSettings.MQTT_CONNECT_TIMEOUT;
                this.client.Settings.AttemptsOnRetry = 0;
                Debug.Log("MqttClient created.");
                Debug.Log(this.client.Settings.ToString());
                Debug.Log(JsonConvert.SerializeObject(this.client.Settings));
            } catch { Debug.Log("Client obj not created"); }

            yield return new WaitForSeconds(1);
            try {
                this.client.Connect(this.clientId, this.mqttUserName, this.mqttPassword);
                Debug.Log("Client connection begun.");
            } catch { Debug.Log("Client cannot be connected."); }

            yield return new WaitForSeconds(1);
            if (this.client.IsConnected) {
                this.client.MqttMsgPublishReceived += this.config.handle_incoming_message;
                Debug.Log("Client is connected.");
            } else { Debug.Log("Client failed to connect."); }
        }

        public void Disconnect() { try { this.client.Disconnect(); } finally { this.client = null; } }
    }
}
