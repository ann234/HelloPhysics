using UnityEngine;
using System.Collections;

public class PipeSnap_straight : MonoBehaviour {

    private GameObject pipe;
    private int SnapLimit = 30;

    void Start()
    {
        pipe = transform.parent.gameObject;
    }

    void OnTriggerStay(Collider cold)
    {
        //if(Global_Variable.curCtrlMode == ControlMode.CONTROL_MODE_CAMERA)
        {
            if (pipe.GetComponent<MchObject>().isPicked)
            {
                Transform transCold = cold.transform;
                float difference = Mathf.Abs(pipe.transform.rotation.eulerAngles.z - transCold.rotation.eulerAngles.z);
                switch (cold.name)
                {
                    case "PipeSpinColliderRight":
                        if (Mathf.Abs(difference) <= SnapLimit ||
                            Mathf.Abs(360 - difference) <= SnapLimit ||
                            Mathf.Abs(180 - difference) <= SnapLimit) //
                        {
                            pipe.transform.position = cold.transform.position;
                            if (gameObject.name == "PipeStraightColliderLeft")
                            {
                                pipe.transform.Translate(new Vector3(0.929f, 0, 0));
                            }
                            else
                            {
                                pipe.transform.Translate(new Vector3(-0.929f, 0, 0));
                            }
                            pipe.transform.rotation = transCold.rotation;
                        }
                        break;
                    case "PipeSpinColliderTop":
                        if (Mathf.Abs(difference) <= SnapLimit ||
                            (Mathf.Abs(90 - difference) <= SnapLimit) ||
                            (Mathf.Abs(difference - 270) <= SnapLimit))
                        {
                            pipe.transform.position = cold.transform.position;
                            if (gameObject.name == "PipeStraightColliderLeft")
                            {
                                pipe.transform.Translate(new Vector3(0.9f, 0, 0));
                                pipe.transform.rotation = Quaternion.Euler(new Vector3(cold.transform.rotation.eulerAngles.x,
                                cold.transform.rotation.eulerAngles.y,
                                cold.transform.rotation.eulerAngles.z + 90));
                            }
                            else
                            {
                                pipe.transform.Translate(new Vector3(-0.9f, 0, 0));
                                pipe.transform.rotation = Quaternion.Euler(new Vector3(cold.transform.rotation.eulerAngles.x,
                                cold.transform.rotation.eulerAngles.y,
                                cold.transform.rotation.eulerAngles.z + 90));
                            }
                        }
                        break;
                }
            }
        }
    }
}
