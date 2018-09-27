using UnityEngine;
using System.Collections;

public class PipeSnap_spin : MonoBehaviour {

    private GameObject pipe;
    private int SnapLimit = 30;

    void Start()
    {
        pipe = transform.parent.gameObject;
    }

    void OnTriggerStay(Collider cold)
    {
        //if(Global_Variable.curCtrlMode == ControlMode.CONTROL_MODE_CAMERA)// && pipe.GetComponent<MchObject>().isPicked)
        if (pipe.GetComponent<MchObject>().isPicked)
        {
            Transform transCold = cold.transform;
            float difference = Mathf.Abs(pipe.transform.rotation.eulerAngles.z - transCold.rotation.eulerAngles.z);

            switch (cold.name)
            {
                case "PipeStraightColliderLeft":
                    if (Mathf.Abs(difference) <= SnapLimit ||   //straight:left - spin:right
                        Mathf.Abs(360 - difference) <= SnapLimit)
                    {
                        pipe.transform.position = transCold.position;
                        pipe.transform.Translate(new Vector3(-0.8f, 0.365f, 0));
                        pipe.transform.rotation = transCold.rotation;
                    }
                    else if(Mathf.Abs(270 - difference) <= SnapLimit)
                    {
                        pipe.transform.position = transCold.position;
                        pipe.transform.Translate(new Vector3(0.402f, -0.848f, 0));
                        pipe.transform.rotation = Quaternion.Euler(new Vector3(transCold.rotation.eulerAngles.x,
                                transCold.rotation.eulerAngles.y,
                                transCold.rotation.eulerAngles.z - 90));
                    }
                    else if(Mathf.Abs(90 - difference) <= SnapLimit)   //straight: left - spin:top
                    {
                        pipe.transform.position = transCold.position;
                        pipe.transform.Translate(new Vector3(0.402f, -0.848f, 0));
                        //pipe.transform.Translate(new Vector3(0.532f, -0.623f, 0));
                        pipe.transform.rotation = Quaternion.Euler(new Vector3(transCold.rotation.eulerAngles.x,
                                transCold.rotation.eulerAngles.y,
                                transCold.rotation.eulerAngles.z - 90));
                    }
                    break;
                case "PipeStraightColliderRight":
                    if (Mathf.Abs(90 - difference) <= SnapLimit)    //straight: right - spin : top
                    {
                        pipe.transform.position = transCold.position;
                        pipe.transform.Translate(new Vector3(0.382f, -0.863f, 0));
                        pipe.transform.rotation = Quaternion.Euler(new Vector3(transCold.rotation.eulerAngles.x,
                                transCold.rotation.eulerAngles.y,
                                transCold.rotation.eulerAngles.z + 90));
                    }
                    else if (Mathf.Abs(180 - difference) <= SnapLimit)   //straight: right - spin:right
                    {
                        pipe.transform.position = transCold.position;
                        pipe.transform.Translate(new Vector3(-0.8f, 0.365f, 0));
                        pipe.transform.rotation = Quaternion.Euler(new Vector3(transCold.rotation.eulerAngles.x,
                                transCold.rotation.eulerAngles.y,
                                transCold.rotation.eulerAngles.z - 180));
                    }
                    break;
            }
        }
    }
}
