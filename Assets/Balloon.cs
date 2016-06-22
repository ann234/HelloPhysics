﻿using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(rb.velocity.magnitude < 2)
        {
            rb.AddForce(new Vector3(0, 1, 0));
        }
	}
}