using UnityEngine;
using System.Collections;

public class ItemOption : MonoBehaviour {

    private GameObject _parentObj;

    public GameObject parentObj
    {
        get
        {
            return _parentObj;
        }
        set
        {
            _parentObj = value;
        }
    }
}
