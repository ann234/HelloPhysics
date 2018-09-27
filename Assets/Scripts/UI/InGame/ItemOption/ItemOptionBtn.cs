using UnityEngine;
using System.Collections;

public class ItemOptionBtn : MonoBehaviour {

    protected GameObject attachedMchObj;

    // Use this for initialization
    public virtual void Start () {
        attachedMchObj = transform.parent.GetComponent<ItemOption>().parentObj;
    }
}
