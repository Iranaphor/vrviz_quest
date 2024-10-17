using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_handler : MonoBehaviour
{
    // Public variables for configuration
    public GameObject pointsPrefab;

    // Configuration properties
    public string Style = "Flat Squares";
    public float Alpha = 1.0f;
    public string ColorStr = "255, 255, 255";
    public float SizeMeters = 0.01f;
    public float DecayTime = 0f;
    public bool UseRainbow = true;
    public bool InvertRainbow = false;
    public float MinIntensity = 0f;
    public float MaxIntensity = 4096f;
    public string Axis = "Z";
    public string ChannelName = "intensity";

    // Internal variables
    public List<GameObject> scanPoints = new List<GameObject>();
    public List<float> pointTimestamps = new List<float>();
    public Gradient intensityGradient;

    void Update()
    {
        HandleDecay();
    }

    public void SetConfig()
    {
        // Initialize the intensity gradient
        if (UseRainbow)
        {
            intensityGradient = new Gradient();
            GradientColorKey[] colorKeys = GenerateRainbowGradient(InvertRainbow);
            intensityGradient.SetKeys(colorKeys, new GradientAlphaKey[] { new GradientAlphaKey(Alpha, 0f) });
        }
        else
        {
            // Use specified MinColor and MaxColor
            intensityGradient = new Gradient();
            Color minColor = ParseColorString(ColorStr);
            Color maxColor = ParseColorString(ColorStr); // Using the same color for simplicity
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0].color = minColor;
            colorKeys[0].time = 0f;
            colorKeys[1].color = maxColor;
            colorKeys[1].time = 1f;
            intensityGradient.SetKeys(colorKeys, new GradientAlphaKey[] { new GradientAlphaKey(Alpha, 0f) });
        }
    }

    public Vector3 CalculatePosition(float range, float angle)
    {
        float x = range * Mathf.Cos(angle);
        float y = 0f;
        float z = range * Mathf.Sin(angle);

        switch (Axis.ToUpper())
        {
            case "X":
                return new Vector3(z, y, x);
            case "Y":
                return new Vector3(x, z, y);
            default: // "Z"
                return new Vector3(x, y, z);
        }
    }

    public void ApplyVisualProperties(GameObject point, float intensity)
    {
        MeshRenderer renderer = point.GetComponent<MeshRenderer>();
        Color color;

        if (UseRainbow)
        {
            float t = Mathf.InverseLerp(MinIntensity, MaxIntensity, intensity);
            color = intensityGradient.Evaluate(t);
        }
        else
        {
            // Since MinColor and MaxColor are the same, t is not needed
            color = intensityGradient.Evaluate(0f);
        }

        color.a = Alpha;
        renderer.material.color = color;
    }

    private void HandleDecay()
    {
        if (DecayTime > 0f)
        {
            float currentTime = Time.time;

            for (int i = 0; i < scanPoints.Count; i++)
            {
                if (scanPoints[i].activeSelf && currentTime - pointTimestamps[i] > DecayTime)
                {
                    // Deactivate the point
                    scanPoints[i].SetActive(false);
                }
            }
        }
    }

    private GradientColorKey[] GenerateRainbowGradient(bool invert)
    {
        Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta, Color.red };
        if (invert)
        {
            System.Array.Reverse(colors);
        }

        GradientColorKey[] colorKeys = new GradientColorKey[colors.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colorKeys[i].color = colors[i];
            colorKeys[i].time = i / (float)(colors.Length - 1);
        }

        return colorKeys;
    }

    // Helper method to parse color strings in the format "R, G, B"
    private Color ParseColorString(string colorStr)
    {
        string[] parts = colorStr.Split(',');
        float r = float.Parse(parts[0].Trim()) / 255f;
        float g = float.Parse(parts[1].Trim()) / 255f;
        float b = float.Parse(parts[2].Trim()) / 255f;
        return new Color(r, g, b, Alpha);
    }
}
