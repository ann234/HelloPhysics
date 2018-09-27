using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Singleton
public class StageManager : MonoBehaviour
{
    enum CurrentScene
    {
        MAINMENU,
        INGAME
    }

    private static StageManager instance;
    private AddObjectManager addObjManager;
    public int _curStage = 0;   //open to public for debugging
    private static int _maxStage;
    private CurrentScene currentScene = CurrentScene.MAINMENU;

    public List<GameObject> addedObjects = new List<GameObject>(); //플레이어가 오브젝트를 생성했을 때 생성된 오브젝트가 보관 될 곳
    public List<GameObject> stages;

    private string[] missionText =
    {
        "Push the red switch!",
        "Get all stars!",
        "Push the red switch!",
        "Push the red switch!",
        "Push the red switch!",
        "Get all stars!"
    };

    public int curStage
    {
        get
        {
            return _curStage;
        }
        set
        {
            _curStage = value;
        }
    }

    public static StageManager getInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType<StageManager>();
        }
        return instance;
    }

    public static int getMaxStage()
    {
        return _maxStage;
    }

    public void resetStage()
    {
        destroyStage();
        StartCoroutine(this.createStage(_curStage));
    }

    public void changeStage(int indexOfStage)
    {
        destroyStage();
        StartCoroutine(this.createStage(indexOfStage));
    }

    public void destroyStage()
    {
        Global_Variable.resetVar();                 //카메라 상태 등등 전역변수 초기화

        /**get all X sprite and ItemOption UI that have been attached to the MchObject and destroy all*/
        foreach(MchObject mchObj in GameObject.FindObjectsOfType<MchObject>())
        {
            
        }
        //foreach (GameObject mchObjUI in GameObject.FindGameObjectsWithTag("MchObjectUI"))
        //{
        //    Destroy(mchObjUI);  //destroy
        //}
        
        Destroy(GameObject.FindWithTag("Stage"));   //현재 스테이지 파괴
        foreach (GameObject obj in addedObjects)    //스테이지에는 없는 동적으로 추가한 오브젝트를 파괴
        {
            Destroy(obj);
            print("object is destroyed");
        }
        
        addedObjects.Clear();                       //동적 추가 오브젝트를 저장하는 List 청소
    }

    public IEnumerator createStage(int indexOfStage)
    {
        if (!stages[indexOfStage])
        {
            print("Can't find stage");
        }
        else
        {
            Instantiate(stages[indexOfStage]);     //새로운 스테이지 생성

            if (!addObjManager)                     //추가 오브젝트 매니저가 없을경우
                addObjManager = AddObjectManager.getInstance(); //가져온다
            //addObjManager.reset();

            _curStage = indexOfStage;   //현재 스테이지 정보를 갱신.

            /**open mission explain window*/
            GameObject mWindow = GameObject.FindObjectOfType<GameManager>().getMissionWindow();
            if (mWindow)
            {
                mWindow.SetActive(true);
                if(stages[indexOfStage].GetComponent<Stage>() != null)
                {
                    mWindow.GetComponentInChildren<UILabel>().text =
                        string.Format("{0}", stages[indexOfStage].GetComponent<Stage>().stageGoal);
                }
                else
                {
                    mWindow.GetComponentInChildren<UILabel>().text =
                        string.Format("Stage {0}", indexOfStage + 1);
                }
            }
            else
            {
                print("StageManager : " + "Can't open missionWindow");
            }

            Camera.main.transform.position = new Vector3(0, 5, -9.5f);
            yield return null;
        }
    }

    void Awake()
    {
        //if(instance)
        //{
        //    Destroy(gameObject);
        //}
        //else
        {
            instance = GameObject.FindObjectOfType<StageManager>();
            DontDestroyOnLoad(gameObject);
        }
        addObjManager = AddObjectManager.getInstance();
        _maxStage = stages.Count;
    }

    void OnLevelWasLoaded()
    {
        if (currentScene == CurrentScene.MAINMENU)
        {
            resetStage();
            currentScene = CurrentScene.INGAME;
        }
        else if (currentScene == CurrentScene.INGAME)
        {
            destroyStage();
            currentScene = CurrentScene.MAINMENU;
        }
    }
}