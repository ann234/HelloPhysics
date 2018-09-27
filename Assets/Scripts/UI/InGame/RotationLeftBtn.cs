using UnityEngine;
using System.Collections;

public class RotationLeftBtn : MonoBehaviour {

	void OnMouseDown()
    {
        print("Rotation Mode");
        Global_Variable.curMode = TransformMode.MODE_ROTATION;
        transform.parent.gameObject.SetActive(false);
    }
}
