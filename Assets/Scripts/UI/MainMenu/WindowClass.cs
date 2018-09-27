using UnityEngine;
using System.Collections;

public class WindowClass : MonoBehaviour {

    private Transform trans;
    private Transform mainTrans;
    private Camera UICamera;

    public void openWindow()
    {
        StartCoroutine(Global_Variable.moveObject(UICamera.transform, trans));
    }

    public void closeWIndow()
    {
        StartCoroutine(Global_Variable.moveObject(UICamera.transform, mainTrans));
    }

    public virtual void Awake()
    {
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        trans = transform.transform;
        mainTrans = GameObject.Find("Main").transform;
    }
}
