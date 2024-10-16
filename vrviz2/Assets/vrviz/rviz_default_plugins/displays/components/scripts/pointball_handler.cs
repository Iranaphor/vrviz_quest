using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointball_handler : MonoBehaviour
{

    public GameObject PointBall;

    public float Alpha = 1.0f;
    public string Color = "10, 200, 10";
    public float Radius = 1.0f;
    public float HistoryLength = 1.0f;

    void Update() {
        //this.SetConfig();
    }

    public void SetConfig(){

        this.PointBall.transform.localScale = new Vector3(this.Radius, this.Radius, this.Radius);

    }

}
