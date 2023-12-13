using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using rviz_utils = VRViz.plugins.rviz_default_plugins.utils;

public class covariance_handler : MonoBehaviour
{

    public GameObject PositionCylinder;
    public GameObject OrientationCone;

    public rviz_utils::Orientation Orientation;
    public rviz_utils::Position Position;
    public bool Value;


    void Update() {
        //this.SetConfig();
    }

    public void SetConfig(){

        // Apply Configuration to Position Cylinder
        this.PositionCylinder.SetActive(this.Position.Value);
        this.PositionCylinder.transform.localScale = new Vector3(0, 0.025f, 0);

        // Apply Configuration to Orientation Cone
        this.OrientationCone.SetActive(this.Orientation.Value);

    }
}
