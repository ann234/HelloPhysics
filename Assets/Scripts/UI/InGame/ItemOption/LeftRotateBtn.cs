using UnityEngine;
using System.Collections;

public class LeftRotateBtn : ItemOptionBtn
{
    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    void OnMouseDown()
    {
        if(attachedMchObj.GetComponent<MchObject>().type == ItemType.BOARD)
        {
            attachedMchObj.transform.Rotate(new Vector3(1, 0, 0) * 15);
        }
        else if (attachedMchObj.GetComponent<MchObject>().type == ItemType.BOARD_SPIN)
        {
            attachedMchObj.transform.Rotate(new Vector3(1, 0, 0) * 15);
        }
        else
            attachedMchObj.transform.Rotate(new Vector3(0, 0, 1) * 15);
    }
}
