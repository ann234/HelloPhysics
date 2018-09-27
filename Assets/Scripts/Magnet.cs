using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Magnet : MonoBehaviour {

    private Rigidbody rb;

    private List<GameObject> ironObjs = new List<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ironObjs = new List<GameObject>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Iron"))
        {
            if (obj != null)
            {
                ironObjs.Add(obj);
            }
        }
        //print("Number of iron objects : " + ironObjs.Count);
    }

	// Update is called once per frame
	void Update () {
        if(Global_Variable.isSimulate)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Iron"))
            {
                if(!obj)
                {
                    print("iron objet null");
                }
                else
                {
                    Rigidbody obj_rb = obj.GetComponent<Rigidbody>();
                    Vector3 offset = rb.position - obj_rb.position;
                    Vector3 dir = offset.normalized;
                    float len = offset.magnitude;
                    if (len < 10)
                    {
                        obj_rb.AddForce(20 * (1 / len) * dir);
                    }
                }
            }
        }
    }
}
