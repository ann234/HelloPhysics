using UnityEngine;
using System.Collections;
using System;

public class OnPunch : ButtonIntf {

    private Collider col;
    private SpringJoint sp;

    public override void switchOn()
    {
        Rigidbody connectedRb = sp.connectedBody;
        connectedRb.isKinematic = false;
        Vector3 dir = (connectedRb.position - rb.position).normalized;
        connectedRb.AddForce(dir * 500);
    }

    // Use this for initialization
    public override void Start()
    {
        col = GetComponent<Collider>();
        rb = col.attachedRigidbody;
        sp = GetComponent<SpringJoint>();
    }

    // Update is called once per frame
    public override void Update () {
	
	}
}
