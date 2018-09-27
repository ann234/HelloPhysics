using UnityEngine;
using System.Collections;

public abstract class WinCondition : MonoBehaviour {

    protected StageManager stageManager;
    protected ClearWindow clearWindow;

    public virtual void Awake()
    {
        clearWindow = GameObject.Find("ClearWindow").GetComponent<ClearWindow>();
    }

    public virtual void Start()
    {
        if (!StageManager.getInstance())
        {
            print("WindowCondition : Can't find StageManager.");
        }
        else
        {
            stageManager = StageManager.getInstance();
        }
    }

    public void afterWin()
    {
        print("win!");
        if(Global_Variable.curPlayer.nClearStage <= stageManager._curStage)
        {
            Global_Variable.curPlayer.nClearStage += 1;
            Global_Variable.curPlayer.saveInfo();
        }
        StartCoroutine(this.openClearWindow());
    }

    IEnumerator openClearWindow()
    {
        yield return new WaitForSeconds(2);
        clearWindow.showWindow();
    }
}
