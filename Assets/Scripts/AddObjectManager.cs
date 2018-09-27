using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class playObject
{
    private uint initNumOfObj;

    public GameObject prefab;
    public uint numOfObj;
    public Vector3 initRot;

    public playObject(GameObject _prefab, uint _numOfObj)
    {
        prefab = _prefab;
        numOfObj = _numOfObj;

        initNumOfObj = _numOfObj;
    }

    public void init()
    {
        initNumOfObj = numOfObj;
    }

    public void reset()
    {
        numOfObj = initNumOfObj;
    }
}

public class AddObjectManager : MonoBehaviour
{

    private static AddObjectManager instance;
    private StageManager stageManager;

    public List<playObject> objs;       //위의 playObject를 모아둔 것. 스테이지에 추가될 오브젝트의 종류와 양을 관리한다.


    public static AddObjectManager getInstance()
    {
        if (!instance)
        {
            instance = GameObject.FindObjectOfType<AddObjectManager>();
        }
        return instance;
    }

    void Awake()
    {
        stageManager = StageManager.getInstance();
        instance = GameObject.FindObjectOfType<AddObjectManager>();

        foreach (playObject pObj in objs)
        {
            pObj.init();
        }
    }

    void Start()
    {
        int count = 0;
        foreach (ClickItem item in GameObject.FindObjectOfType<GameManager>().clickItems)
        {
            //get all ClickItem and set false
            item.gameObject.SetActive(false);

            //스테이지에 필요한 오브젝트 아이템 UI만 켜줌.
            if (setItemUI(item, count))
            {
                count++;
            }
        }
    }

    public bool setItemUI(ClickItem item, int count)
    {
        /**Find itemUI such as additem */
        foreach (playObject pObj in objs)
        {
            /**compare add object's type and itemUI's item type*/
            if (pObj.prefab.GetComponent<MchObject>().type == item.type)
            {
                GameObject gObj = item.gameObject;

                item.item = pObj;
                item.index = count;

                gObj.GetComponent<UISprite>().leftAnchor.absolute = 40 + count * 120;
                gObj.GetComponent<UISprite>().rightAnchor.absolute = 140 + count * 120;

                item.gameObject.SetActive(true);
                item.refresh(true);

                return true;
            }
        }
        return false;
    }

    public void reset()
    {
        foreach (playObject pObj in objs)
        {
            pObj.reset();
        }
    }
}
