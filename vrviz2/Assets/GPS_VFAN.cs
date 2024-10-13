using System.IO.Ports;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPS_VFAN : MonoBehaviour
{
    public string portName;
    public int baudRate;
    private SerialPort serialPort;
    private bool isPortOpen = false;

    public Text text_log;

    void Start()
    {
        this.text_log.text = "GPSVFAN - 1";
        OpenSerialPort();
        this.text_log.text = "GPSVFAN - 2";
        StartCoroutine(ReadSerialData());
        this.text_log.text = "GPSVFAN - 3";
    }

    void OpenSerialPort()
    {
        this.text_log.text = "GPSVFAN - 1 - a --- connecting to " + this.portName;
        serialPort = new SerialPort(this.portName, this.baudRate);
        this.text_log.text = "GPSVFAN - 1 - b";
        try
        {
            this.text_log.text = "GPSVFAN - 1 - c";
            serialPort.Open();
            this.text_log.text = "GPSVFAN - 1 - d";
            isPortOpen = true;
            Debug.Log("Serial port opened successfully.");
            this.text_log.text = "Serial port opened successfully.";
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not open serial port: " + e.Message);
            this.text_log.text = "Could not open serial port: " + e.Message;
        }
        this.text_log.text = "GPSVFAN - 1 - e";
    }

    IEnumerator ReadSerialData()
    {
        while (isPortOpen)
        {
            if (serialPort != null && serialPort.IsOpen && serialPort.BytesToRead > 0)
            {
                try
                {
                    string data = serialPort.ReadLine();
                    ParseNMEASentence(data);
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning("Error reading from serial port: " + e.Message);
                    this.text_log.text = "Error reading from serial port: " + e.Message;
                }
            }
            yield return null; // Wait a frame before checking again.
        }
    }

    void ParseNMEASentence(string sentence)
    {
        // Check if the sentence is a GPGGA sentence
        if (sentence.StartsWith("$GPGGA"))
        {
            // Split the sentence into its components
            string[] components = sentence.Split(',');
            if (components.Length >= 5) // Make sure it has enough parts
            {
                // Extract latitude and longitude
                string latitude = components[2];
                string longitude = components[4];
                Debug.Log($"Latitude: {latitude}, Longitude: {longitude}");
                this.text_log.text = $"Latitude: {latitude}, Longitude: {longitude}";
            }
        }
    }

    void OnDisable()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            isPortOpen = false;
            Debug.Log("Serial port closed.");
            this.text_log.text = "Serial port closed.";
        }
    }
}
