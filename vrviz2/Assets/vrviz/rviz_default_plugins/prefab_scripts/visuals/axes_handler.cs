using UnityEngine;

public class axes_handler : MonoBehaviour
{

    public Transform GreenAxis;
    public Transform RedAxis;
    public Transform BlueAxis;

    public void SetConfig(float AxesLength, float AxesRadius){
        this.GreenAxis.localScale = new Vector3(AxesRadius, AxesLength, AxesRadius);
        this.RedAxis.localScale = new Vector3(AxesRadius, AxesLength, AxesRadius);
        this.BlueAxis.localScale = new Vector3(AxesRadius, AxesLength, AxesRadius);
        
        this.GreenAxis.localPosition = new Vector3(0, AxesLength, 0);
        this.RedAxis.localPosition = new Vector3(AxesLength, 0, 0);
        this.BlueAxis.localPosition = new Vector3(0, 0, AxesLength);
    }
}
