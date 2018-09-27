using UnityEngine;
using System.Collections;

public class ClearWindow : MonoBehaviour {

    private Transform mainPos;

    public Transform initPos;

    public void showWindow()
    {
        StartCoroutine(Global_Variable.moveObject(transform, mainPos));
    }

    public void closeWindow()
    {
        StartCoroutine(Global_Variable.moveObject(transform, initPos));
    }

	// Use this for initialization
	void Start () {
        mainPos = GameObject.Find("UI Root").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
