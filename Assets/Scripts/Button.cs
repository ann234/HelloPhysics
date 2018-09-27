using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public float needForce = 2.0f;
    public ButtonIntf btn;
    
    private bool isButtonOn;

    IEnumerator btnMove()
    {
        while(GetComponent<Transform>().localPosition.y > 0.5f)
        {
            GetComponent<Transform>().Translate(new Vector3(0, -0.05f, 0));
            yield return null;
        }
    }

    void ButtonDown()
    {
        isButtonOn = true;
        if(btn != null)
        {
            btn.switchOn();
        }
        StartCoroutine(this.btnMove());
    }

    void OnCollisionEnter(Collision cols)
    {
        if(cols.relativeVelocity.magnitude > needForce && !isButtonOn)
        {
            ButtonDown();
        }
    }

    // Use this for initialization
    void Start()
    {
        isButtonOn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
