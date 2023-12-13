using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowaxes_handler : MonoBehaviour
{
    public GameObject arrow;
    public GameObject axes;

    public string Shape = "arrow";
    public float Alpha = 1.0f;
    public string Color = "10, 200, 10";
    public float HeadLength = 1.0f;
    public float HeadRadius = 1.0f;
    public float ShaftLength = 1.0f;
    public float ShaftRadius = 1.0f;
    public float AxesLength = 1.0f;
    public float AxesRadius = 1.0f;

    void Update() {
        //this.SetConfig();
    }

    public void SetConfig(){

        if (this.Shape == "axes"){
            this.axes.SetActive(true);
            this.arrow.SetActive(false);
        } else if (this.Shape == "arrow"){
            this.axes.SetActive(false);
            this.arrow.SetActive(true);
        }

        this.axes.GetComponent<axes_handler>().SetConfig(this.AxesLength, this.AxesRadius);
        this.arrow.GetComponent<arrow_handler>().SetConfig(this.Alpha, this.Color, this.HeadLength, this.HeadRadius, this.ShaftLength, this.ShaftRadius);

    }
}
