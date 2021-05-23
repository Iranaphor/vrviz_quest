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
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

/// <summary>
/// Adaptation for Unity of the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// modified to run on UWP (also tested on Microsoft HoloLens).
/// </summary>
namespace M2MqttUnity
{
    public class M2MqttUnityClient : MonoBehaviour
    {
        [Header("MQTT broker configuration")]
        public string WANBrokerAddress = "86.169.236.130";
        public int WANBrokerPort = 55555;
        public string LANBrokerAddress = "192.168.1.93";
        public int LANBrokerPort = 8883;
        protected string brokerAddress;
        protected int brokerPort;
        
        public string test1 = "WAN";
        public string test2 = "LAN";
        protected int total_tests = 0;


        [Header("Connection parameters")]
        public bool isEncrypted = false;
        public int connectionDelay = 500;
        public int timeoutOnConnection = MqttSettings.MQTT_CONNECT_TIMEOUT; //timeout in milliseconds
        public bool autoConnect = false;
        public string mqttUserName = null;
        public string mqttPassword = null;
        
        protected MqttClient client;

        private List<MqttMsgPublishEventArgs> messageQueue1 = new List<MqttMsgPublishEventArgs>();
        private List<MqttMsgPublishEventArgs> messageQueue2 = new List<MqttMsgPublishEventArgs>();
        private List<MqttMsgPublishEventArgs> frontMessageQueue = null;
        private List<MqttMsgPublishEventArgs> backMessageQueue = null;
        private bool mqttClientConnectionClosed = false;
        private bool mqttClientConnected = false;

        public event Action ConnectionSucceeded;
        public event Action ConnectionFailed;

        public virtual void Connect() {
            if (client == null || !client.IsConnected) {
                
                total_tests++;
                string test = (total_tests <= 1) ? test1 : test2;
                
                switch (test) {
                    case "WAN":
                        Debug.Log("Connecting to WAN");
                        brokerAddress = WANBrokerAddress;
                        brokerPort = WANBrokerPort;
                        break;

                    case "LAN":
                        Debug.Log("Connecting to LAN");
                        brokerAddress = LANBrokerAddress;
                        brokerPort = LANBrokerPort;
                        break;

                    default:
                        break;
                }

                StartCoroutine(DoConnect());
            }
        }

        public virtual void Disconnect() {
            if (client != null) {
                StartCoroutine(DoDisconnect());
            }
        }

        protected virtual void OnConnecting() {
            Debug.LogFormat("Connecting to broker on {0}:{1}...\n", brokerAddress, brokerPort.ToString());
        }

        protected virtual void OnConnected() {
            Debug.LogFormat("Connected to {0}:{1}...\n", brokerAddress, brokerPort.ToString());
            
            SubscribeTopics();

            if (ConnectionSucceeded != null) {
                ConnectionSucceeded();
            }
        }

        protected virtual void OnConnectionFailed(string errorMessage) {
            Debug.LogWarning("Connection failed.");

            if (total_tests < 2) Connect();

            if (ConnectionFailed != null) ConnectionFailed();
        }

        protected virtual void SubscribeTopics() {}
        protected virtual void UnsubscribeTopics() {}

        protected virtual void OnApplicationQuit() { CloseConnection(); }

        protected virtual void Awake()
        {
            frontMessageQueue = messageQueue1;
            backMessageQueue = messageQueue2;
        }

        protected virtual void Start()
        {
            if (autoConnect)
            {
                Connect();
            }
        }

        /// <summary>
        /// Override this method for each received message you need to process.
        /// </summary>
        protected virtual void DecodeMessage(string topic, byte[] message)
        {
            Debug.LogFormat("Message received on topic: {0}", topic);
        }

        protected virtual void OnDisconnected() { Debug.Log("Disconnected."); }
        protected virtual void OnConnectionLost() { Debug.LogWarning("CONNECTION LOST!"); }

        /// <summary>
        /// Processing of income messages and events is postponed here in the main thread.
        /// Remember to call ProcessMqttEvents() in Update() method if you override it.
        /// </summary>
        protected virtual void Update()
        {
            ProcessMqttEvents();
        }

        protected virtual void ProcessMqttEvents()
        {
            // process messages in the main queue then messages income in the meanwhile
            for (int i = 0; i <= 1; i++) {
                SwapMqttMessageQueues();
                ProcessMqttMessageBackgroundQueue();
            }
            if (mqttClientConnectionClosed)
            {
                mqttClientConnectionClosed = false;
                OnConnectionLost();
            }
        }

        private void ProcessMqttMessageBackgroundQueue()
        {
            foreach (MqttMsgPublishEventArgs msg in backMessageQueue)
            {
                DecodeMessage(msg.Topic, msg.Message);
            }
            backMessageQueue.Clear();
        }

        /// <summary>
        /// Swap the message queues to continue receiving message when processing a queue.
        /// </summary>
        private void SwapMqttMessageQueues()
        {
            frontMessageQueue = frontMessageQueue == messageQueue1 ? messageQueue2 : messageQueue1;
            backMessageQueue = backMessageQueue == messageQueue1 ? messageQueue2 : messageQueue1;
        }

        private void OnMqttMessageReceived(object sender, MqttMsgPublishEventArgs msg) { frontMessageQueue.Add(msg); }
        private void OnMqttConnectionClosed(object sender, EventArgs e) {
            // Set unexpected connection closed only if connected (avoid event handling in case of controlled disconnection)
            mqttClientConnectionClosed = mqttClientConnected;
            mqttClientConnected = false;
        }

        /// <summary>
        /// Connects to the broker using the current settings.
        /// </summary>
        /// <returns>The execution is done in a coroutine.</returns>
        private IEnumerator DoConnect()
        {
            // wait for the given delay
            yield return new WaitForSecondsRealtime(connectionDelay / 1000f);
            // leave some time to Unity to refresh the UI
            yield return new WaitForEndOfFrame();

            // create client instance 
            if (client == null)
            {
                try
                {
#if (!UNITY_EDITOR && UNITY_WSA_10_0 && !ENABLE_IL2CPP)
                    client = new MqttClient(brokerAddress,brokerPort,isEncrypted, isEncrypted ? MqttSslProtocols.SSLv3 : MqttSslProtocols.None);
#else
                    client = new MqttClient(brokerAddress, brokerPort, isEncrypted, null, null, isEncrypted ? MqttSslProtocols.SSLv3 : MqttSslProtocols.None);
                    //System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate();
                    //client = new MqttClient(brokerAddress, brokerPort, isEncrypted, cert, null, MqttSslProtocols.TLSv1_0, MyRemoteCertificateValidationCallback);
#endif
                }
                catch (Exception e)
                {
                    client = null;
                    Debug.LogErrorFormat("CONNECTION FAILED! {0}", e.ToString());
                    OnConnectionFailed(e.Message);
                    yield break;
                }
            }
            else if (client.IsConnected)
            {
                yield break;
            }
            OnConnecting();  //TODO: this was the last point before delay

            // leave some time to Unity to refresh the UI
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            // int sec = 1000;
            client.Settings.TimeoutOnConnection = timeoutOnConnection;
            // client.Settings.TimeoutOnReceiving = 30*sec; // default timeout on receiving from client
            // client.Settings.AttemptsOnRetry = 1; // max publish, subscribe and unsubscribe retry for QoS Level 1 or 2
            // client.Settings.DelayOnRetry = 10*sec; // delay for retry publish, subscribe and unsubscribe for QoS Level 1 or 2
            // client.Settings.TimeoutOnConnection = 30*sec; // broker need to receive the first message (CONNECT) within a reasonable amount of time after TCP/IP connection 

            string clientId = Guid.NewGuid().ToString();
            try
            {
                client.Connect(clientId, mqttUserName, mqttPassword);
            }
            catch (Exception e)
            {
                client = null;
                Debug.LogErrorFormat("Failed to connect to {0}:{1}:\n{2}", brokerAddress, brokerPort, e.ToString()); //TODO: this was the first message after delay
                OnConnectionFailed(e.Message);
                yield break;
            }
            if (client.IsConnected)
            {
                client.ConnectionClosed += OnMqttConnectionClosed;
                // register to message received 
                client.MqttMsgPublishReceived += OnMqttMessageReceived;
                mqttClientConnected = true;
                OnConnected();
            }
            else
            {
                OnConnectionFailed("CONNECTION FAILED!");
            }
        }

        private IEnumerator DoDisconnect()
        {
            yield return new WaitForEndOfFrame();
            CloseConnection();
            OnDisconnected();
        }

        private void CloseConnection()
        {
            mqttClientConnected = false;
            if (client != null)
            {
                if (client.IsConnected)
                {
                    UnsubscribeTopics();
                    client.Disconnect();
                }
                client.MqttMsgPublishReceived -= OnMqttMessageReceived;
                client.ConnectionClosed -= OnMqttConnectionClosed;
                client = null;
            }
        }

#if ((!UNITY_EDITOR && UNITY_WSA_10_0))
        private void OnApplicationFocus(bool focus)
        {
            // On UWP 10 (HoloLens) we cannot tell whether the application actually got closed or just minimized.
            // (https://forum.unity.com/threads/onapplicationquit-and-ondestroy-are-not-called-on-uwp-10.462597/)
            if (focus)
            {
                Connect();
            }
            else
            {
                CloseConnection();
            }
        }
#endif
    }
}
