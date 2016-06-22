using UnityEngine;
using System.Collections;

public class OnGravity : MonoBehaviour
{
    private Collider col;
    private Rigidbody rb;
    private PhysicMaterial pm;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider>();
        rb = col.attachedRigidbody;
        pm = col.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("s");
            if (rb.useGravity == true)
            {
                print("false");
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                print("true");
                rb.useGravity = true;
            }
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            if(pm.bounciness < 0.9)
            {
                pm.bounciness += 0.1f;
            }
        }
    }
}
