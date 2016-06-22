using UnityEngine;
using System.Collections;

public class GetForce : MonoBehaviour {

    private Rigidbody rb;
    private Collider col;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider>();
        rb = col.attachedRigidbody;
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter(Collision cols)
    {
        if (cols.relativeVelocity.magnitude > 5)
        {
            print("forces!");
            rb.isKinematic = false;
        }
    }
}
