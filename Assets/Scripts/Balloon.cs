using UnityEngine;
using System.Collections;

public class Balloon : MchObject {

    public float force2Up = 4;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    public override void Update () {
        base.Update();
        rb.useGravity = false;
        
        if(rb.velocity.magnitude < force2Up && Global_Variable.isSimulate)
        {
            rb.AddForce(new Vector3(0, 2, 0));
        }
	}
}
