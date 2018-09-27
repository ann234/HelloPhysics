using UnityEngine;
using System.Collections;

public class main : MonoBehaviour {

    private UIPlayTween[] tweener;

    void Awake()
    {
        tweener = new UIPlayTween[2];
        int count = 0;
        foreach (UIPlayTween tw in GetComponents<UIPlayTween>())
        {
            tweener[count] = tw;
            count++;
        }
    }
 
// Use this for initialization
void Start () {
        if (Global_Variable.isLogin)
        {
            tweener[1].Play(true); //main
            tweener[0].Play(true); //move select stage

            //Open stages that user completed
            int count = Global_Variable.curPlayer.nClearStage;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("StageButton"))
            {
                //if stage is cleared
                obj.GetComponent<OpenStage>().refresh(count >= 0);

                count -= 1;
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
