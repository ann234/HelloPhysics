using UnityEngine;
using System.Collections;

public class GoBack : MonoBehaviour {
    private SelectUserWindow selectUserWindow;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    }

   void OnClick()
    {
        FindObjectOfType<SelectUserWindow>().openWindow();
    }

    void Awake()
    {
        if (!GameObject.FindObjectOfType<SelectUserWindow>())
        {
            print("Can't find SelectUserWindow");
        }
        else
        {
            selectUserWindow = FindObjectOfType<SelectUserWindow>();
        }
    }
}
