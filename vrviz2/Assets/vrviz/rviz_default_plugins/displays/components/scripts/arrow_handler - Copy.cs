using UnityEngine;

public class arrow_handler : MonoBehaviour
{

    public Transform Head;
    public Transform Shaft;

    public void SetConfig(float Alpha, string Color, float HeadLength, float HeadRadius, float ShaftLength, float ShaftRadius){
        
        this.Head.localScale = new Vector3(HeadRadius, HeadLength, HeadRadius);
        this.Shaft.localScale = new Vector3(ShaftRadius, ShaftLength/2, ShaftRadius);
        
        this.Head.localPosition = new Vector3(this.Head.localPosition.x, ShaftLength, this.Head.localPosition.z);
        this.Shaft.localPosition = new Vector3(this.Shaft.localPosition.x, ShaftLength / 2, this.Shaft.localPosition.z);
    }
}
