using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

    private Rigidbody rb;

    private GameObject[] ironObjs;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if(ironObjs == null)
        {
            ironObjs = GameObject.FindGameObjectsWithTag("Iron");
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject obj in ironObjs)
        {
            Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
            Vector3 offset = rb.position - obj_rb.position;
            if(offset.magnitude < 10 && offset.magnitude > 1)
            {
                obj_rb.AddForce(offset.normalized * 5);
            }
        }

    }
}
