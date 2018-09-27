using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartButton : MonoBehaviour {

    private StageManager stageManager;
    private List<MchObject> mchObjects = new List<MchObject>();

    public UILabel startLabel;

    // Use this for initialization
    void Awake () {
        stageManager = StageManager.getInstance();
        startLabel.text = "Start";
    }

    // Update is called once per frame
    void Update () {
	
	}

    public virtual void Click()
    {
        //Resetting
        if(Global_Variable.isSimulate)
        {
            stageManager.resetStage();
            startLabel.text = "Start";
        }
        //Starting
        else
        {
            foreach (ItemOption itemOption in GameObject.FindObjectsOfType<ItemOption>())
            {
                itemOption.gameObject.SetActive(false);
            }

            mchObjects.Clear();
            mchObjects.AddRange(GameObject.FindObjectsOfType<MchObject>());   //모든 MchObject 타입의 오브젝트를 가져온다.
            if (Global_Variable.collideObj <= 0)
            {
                foreach (MchObject obj in mchObjects)
                {
                    obj.Simulation(!Global_Variable.isSimulate);    //모든 MchObject의 상태를 시뮬레이션 상태로 전환
                    startLabel.text = "Reset";
                }
                Global_Variable.isSimulate = !Global_Variable.isSimulate;   //전역변수 isSimulate를 변경한다.
            }
        }
    }
}
