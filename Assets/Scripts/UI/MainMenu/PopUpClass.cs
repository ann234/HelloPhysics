using UnityEngine;
using System.Collections;

public class PopUpClass : MonoBehaviour {

    public void onPopUp()
    {
        gameObject.SetActive(true);
    }

    public void offPopUp()
    {
        gameObject.SetActive(false);
    }
}
