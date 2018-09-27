using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {
    
    private WinConditionStar wc;

    void Awake()
    {
        if(GameObject.FindObjectOfType<WinConditionStar>())
        {
            wc = GameObject.FindObjectOfType<WinConditionStar>();
        }
        else
        {
            print("Star : Can't find WinConditionStar");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(Global_Variable.isSimulate)
        {
            wc.getStar();
            Destroy(gameObject);
            //gameObject.SetActive(false);
        } 
    }

    void Update()
    {
        transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 100);
    }
}
