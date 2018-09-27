using UnityEngine;
using System.Collections;

public class option : MonoBehaviour
{

    private OptionWindow optionwindow;
    bool on;

    // Use this for initialization
    void Start()
    {
        on = false;
        optionwindow.offPopUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        on = !on;

        if (on) optionwindow.onPopUp();
        else optionwindow.offPopUp();

    }

    void Awake()
    {
        if (!FindObjectOfType<OptionWindow>())
        {
            print("Can't find OptionWindow");
        }
        else
        {
            optionwindow = FindObjectOfType<OptionWindow>();
        }
    }
}