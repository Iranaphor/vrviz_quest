/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace M2MqttUnity.Examples
{
    /// <summary>
    /// Script for testing M2MQTT with a Unity UI
    /// </summary>
    public class M2MqttUnityTest : M2MqttUnityClient {

        [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
        public bool autoTest = false;
        // public bool once = true;
        
        [Header("User Interface")]
        public InputField consoleInputField;
        public Toggle encryptedToggle;
        public InputField addressInputField;
        public InputField portInputField;
        public Button connectButton;
        public Button disconnectButton;
        public Button testPublishButton;
        public Button clearButton;
        private bool updateUI = false;                       
        private List<string> eventMessages = new List<string>();

        protected List<string[]> topics = new List<string[]> {};


        protected override void Start() { SetUiMessage("Ready."); base.Start(); }
        protected override void Update() { base.Update(); if (updateUI) UpdateUI(); } // call ProcessMqttEvents()

        public void TestPublish() { client.Publish("M2MQTT_Unity/test", System.Text.Encoding.UTF8.GetBytes("Test message"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false); AddUiMessage("Test message published."); }
        public void SetBrokerAddress(string brokerAddress){if (addressInputField && !updateUI) this.brokerAddress = brokerAddress;}
        public void SetBrokerPort(string brokerPort) { if (portInputField && !updateUI) int.TryParse(brokerPort, out this.brokerPort); }
        public void SetEncrypted(bool isEncrypted) { this.isEncrypted = isEncrypted; }
        public void SetUiMessage(string msg) {if (consoleInputField != null) {consoleInputField.text = msg; updateUI = true;}}
        public void AddUiMessage(string msg) {if (consoleInputField != null) {consoleInputField.text += msg + "\n";updateUI = true;}}
        protected override void OnConnecting() { base.OnConnecting(); SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");}
        protected override void OnConnected() {base.OnConnected();SetUiMessage("Connected to broker on " + brokerAddress + "\n");if (autoTest) {TestPublish();}}
        protected override void OnDisconnected(){AddUiMessage("Disconnected.");}
        protected override void OnConnectionLost() { AddUiMessage("CONNECTION LOST!"); }
        private void OnDestroy() { Disconnect(); }
        private void OnValidate() {if (autoTest) autoConnect = true; }        
        
        protected override void OnConnectionFailed(string errorMessage) {
            AddUiMessage("CONNECTION FAILED! " + errorMessage);
        
            if (total_tests < 2) Connect();
        }



        private void UpdateUI() {
            if (client == null) {
                if (connectButton != null) {
                    connectButton.interactable = true;
                    disconnectButton.interactable = false;
                    testPublishButton.interactable = false;
                }
            }
            else {
                if (testPublishButton != null) testPublishButton.interactable = client.IsConnected;
                if (disconnectButton != null) disconnectButton.interactable = client.IsConnected;
                if (connectButton != null) connectButton.interactable = !client.IsConnected;
            }
            if (addressInputField != null && connectButton != null) {
                addressInputField.interactable = connectButton.interactable;
                addressInputField.text = brokerAddress;
            }
            if (portInputField != null && connectButton != null) {
                portInputField.interactable = connectButton.interactable;
                portInputField.text = brokerPort.ToString();
            }
            if (encryptedToggle != null && connectButton != null) {
                encryptedToggle.interactable = connectButton.interactable;
                encryptedToggle.isOn = isEncrypted;
            }
            if (clearButton != null && connectButton != null) {
                clearButton.interactable = connectButton.interactable;
            }
            updateUI = false;
        }


        protected override void UnsubscribeTopics() {
            foreach (string[] t in topics) {
                string factory = t[0];
                string topic = factory;

                if (factory == "__dynamic_server") {
                    string mqtt_topic = t[2];
                    topic = mqtt_topic;
                }
                
                client.Unsubscribe(new string[] { topic });
                Debug.Log("Unsubscribed from Subscriber: " + topic);
            }
        }
        
    }
}
