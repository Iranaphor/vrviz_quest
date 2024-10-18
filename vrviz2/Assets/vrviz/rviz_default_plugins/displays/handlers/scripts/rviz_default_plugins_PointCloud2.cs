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
public class rviz_default_plugins_PointCloud2 : rviz_prefabs.RvizPrefabBase
{

    // 3. CHANGE TO APPROPRIATE PLUGIN MSG TYPE
    public rviz_plugins.PointCloud2 config_data;

    // 4. CHANGE TO APPROPRIATE DATA MSG TYPE
    public sensor_msgs.PointCloud2 message_data;

    // 5. ADD ANY GAMEOBJECTS TO INTERACT WITH HERE (THEY WILL BE ATTACHED TO THIS AS A PREFAB)
    public GameObject PointCloud2Handler;


    public override void on_config_message(rviz_general.Display msg) {
        this.log("New config identified.");


        // 6. SET THE CAST TYPE TO THE APPROPRIATE PLUGIN MSG TYPE
        // save message to associated display
        this.config_data = (rviz_plugins.PointCloud2)msg; 
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
        Type msgtype = Type.GetType("VRViz.Messages.sensor_msgs.PointCloud2", true);
        var json = JsonConvert.DeserializeObject(msgdata, msgtype);
        
        // 8. SET THE CAST TYPE TO THE APPROPRIATE DATA MSG TYPE
        // save message to associated display
        this.message_data = (sensor_msgs.PointCloud2)json;
        this.has_new_msg = true;
    }

    // Respond to recieved config
    public override void apply_new_config() {
        //spawn game object of arrow or axes and sets appearence
        this.log("new config being applied of type DEFAULT");

        // 9. GET GAMEOBJECT
        point_handler handler = this.PointCloud2Handler.GetComponent<point_handler>();

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

    // Respond to received message
    public override void apply_new_msg() {
        this.has_new_msg = false;

        if (this.message_data == null)
        {
            Debug.LogError("this.message_data is null");
            return;
        }

        // Get the point_handler component
        point_handler handler = this.PointCloud2Handler.GetComponent<point_handler>();
        ParticleSystem particleSystem = handler.GetComponent<ParticleSystem>();

        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem component not found on PointCloud2Handler.");
            return;
        }

        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.startLifetime = Mathf.Infinity;

        // Set particle size
        mainModule.startSize = handler.SizeMeters;

        // Get the ParticleSystem's Particle array
        int numPoints = (int)(this.message_data.width.data * this.message_data.height.data);
        mainModule.maxParticles = numPoints;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numPoints];

        // Build a dictionary of field offsets and datatypes
        Dictionary<string, (int offset, int datatype)> fieldDict = new Dictionary<string, (int, int)>();
        foreach (var field in this.message_data.fields)
        {
            fieldDict[field.name.data] = ((int)field.offset.data, (int)field.datatype.data);
        }

        // Check if x, y, z fields are present
        if (!fieldDict.ContainsKey("x") || !fieldDict.ContainsKey("y") || !fieldDict.ContainsKey("z"))
        {
            Debug.LogError("PointCloud2 message does not contain x, y, z fields");
            return;
        }

        int pointStep = (int)this.message_data.point_step.data;
        bool isBigEndian = this.message_data.is_bigendian.data;

        byte[] dataBytes = this.message_data.data;

        // Check data length
        int expectedDataLength = numPoints * pointStep;
        if (dataBytes.Length != expectedDataLength)
        {
            Debug.LogError($"Data length mismatch: expected {expectedDataLength}, got {dataBytes.Length}");
            return;
        }

        float minIntensity = float.MaxValue;
        float maxIntensity = float.MinValue;

        if (fieldDict.ContainsKey("intensity"))
        {
            // First pass to find min and max intensity
            for (int i = 0; i < numPoints; i++)
            {
                int pointBase = i * pointStep;
                float intensity = (float)ReadValueFromData(
                    dataBytes,
                    pointBase + fieldDict["intensity"].offset,
                    fieldDict["intensity"].datatype,
                    isBigEndian
                );
                if (intensity < minIntensity) minIntensity = intensity;
                if (intensity > maxIntensity) maxIntensity = intensity;
            }
        }
        else
        {
            minIntensity = 0f;
            maxIntensity = 1f;
        }

        int particleIndex = 0;

        for (int i = 0; i < numPoints; i++)
        {
            int pointBase = i * pointStep;

            float x = (float)ReadValueFromData(dataBytes, pointBase + fieldDict["x"].offset, fieldDict["x"].datatype, isBigEndian);
            float y = (float)ReadValueFromData(dataBytes, pointBase + fieldDict["y"].offset, fieldDict["y"].datatype, isBigEndian);
            float z = (float)ReadValueFromData(dataBytes, pointBase + fieldDict["z"].offset, fieldDict["z"].datatype, isBigEndian);

            // Check if x, y, z are valid
            if (float.IsNaN(x) || float.IsInfinity(x) ||
                float.IsNaN(y) || float.IsInfinity(y) ||
                float.IsNaN(z) || float.IsInfinity(z))
            {
                continue;
            }

            Color particleColor = Color.white;

            // Check for RGB fields
            if (fieldDict.ContainsKey("red") && fieldDict.ContainsKey("green") && fieldDict.ContainsKey("blue"))
            {
                // Debug.Log("Colour is RGB");
                float r = (float)ReadValueFromData(dataBytes, pointBase + fieldDict["red"].offset, fieldDict["red"].datatype, isBigEndian) / 255f;
                float g = (float)ReadValueFromData(dataBytes, pointBase + fieldDict["green"].offset, fieldDict["green"].datatype, isBigEndian) / 255f;
                float b = (float)ReadValueFromData(dataBytes, pointBase + fieldDict["blue"].offset, fieldDict["blue"].datatype, isBigEndian) / 255f;
                // Debug.Log($"RGB: ({r}, {g}, {b})");

                particleColor = new Color(r, g, b, 1.0f);
            }
            else if (fieldDict.ContainsKey("intensity"))
            {
                Debug.Log("Colour is Intensity");
                // Read intensity
                float intensity = (float)ReadValueFromData(dataBytes, pointBase + fieldDict["intensity"].offset, fieldDict["intensity"].datatype, isBigEndian);

                // Normalize intensity
                if (maxIntensity > minIntensity) {
                    intensity = (intensity - minIntensity) / (maxIntensity - minIntensity);
                } else {
                    intensity = 1.0f; // Avoid division by zero
                }

                // Apply intensity to color
                particleColor = new Color(intensity, intensity, intensity, 1.0f);
            }

            // Create particle
            ParticleSystem.Particle particle = new ParticleSystem.Particle();
            particle.position = new Vector3(x, y, z);
            particle.startColor = particleColor;
            particle.startSize = handler.SizeMeters;
            particle.remainingLifetime = Mathf.Infinity;

            particles[particleIndex] = particle;
            particleIndex++;
        }

        // Clear existing particles
        particleSystem.Clear();

        // Set particles to the particle system
        particleSystem.SetParticles(particles, particleIndex);
    }




    private double ReadValueFromData(byte[] data, int offset, int datatype, bool isBigEndian)
    {
        switch (datatype)
        {
            case 1: // INT8
                return (sbyte)data[offset];
            case 2: // UINT8
                return data[offset];
            case 3: // INT16
                return BitConverter.ToInt16(GetSubArray(data, offset, 2, isBigEndian), 0);
            case 4: // UINT16
                return BitConverter.ToUInt16(GetSubArray(data, offset, 2, isBigEndian), 0);
            case 5: // INT32
                return BitConverter.ToInt32(GetSubArray(data, offset, 4, isBigEndian), 0);
            case 6: // UINT32
                return BitConverter.ToUInt32(GetSubArray(data, offset, 4, isBigEndian), 0);
            case 7: // FLOAT32
                return BitConverter.ToSingle(GetSubArray(data, offset, 4, isBigEndian), 0);
            case 8: // FLOAT64
                return BitConverter.ToDouble(GetSubArray(data, offset, 8, isBigEndian), 0);
            default:
                throw new Exception("Unsupported datatype: " + datatype);
        }
    }

    // Helper function to handle endianess
    private byte[] GetSubArray(byte[] data, int index, int length, bool isBigEndian)
    {
        byte[] result = new byte[length];
        Array.Copy(data, index, result, 0, length);
        if (BitConverter.IsLittleEndian != !isBigEndian)
        {
            Array.Reverse(result);
        }
        return result;
    }


}
