using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

    private GameObject plane;

    void Awake()
    {
        plane = GameObject.Find("Plane");
    }

    void OnTriggerEnter(Collider cold)
    {
        if(cold.attachedRigidbody)
        {
            float mag = cold.attachedRigidbody.velocity.magnitude;
            cold.attachedRigidbody.AddForce(transform.up * mag * -1000);
        }
    }
}