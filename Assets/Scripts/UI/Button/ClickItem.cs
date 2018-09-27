using UnityEngine;
using System.Collections;

public class ClickItem : MonoBehaviour {

    private StageManager stageManager;
    private AddObjectManager addObjManager;
    private UIButton uiButton;
    private UILabel nItemLabel;             //Show the remaining items

    public playObject item;
    public int index = 0;
    public ItemType type = ItemType.NULL;

	// Use this for initialization
	void Awake ()
    {
        if(gameObject.GetComponentInChildren<UILabel>())
        {
            nItemLabel = gameObject.GetComponentInChildren<UILabel>();
            nItemLabel.text = item.numOfObj.ToString();
        }
        else
        {
            print("ClickItem : Can't get UILabel");
        }
        //if(AddObjectManager.getInstance())
        //{
        //    addObjManager = AddObjectManager.getInstance();
        //    if (addObjManager.objs.Count > index)    //만들 수 있는 오브젝트 종류의 갯수가 인덱스보다 많을 경우
        //    {
        //        if (addObjManager.objs[index] != null)
        //        {
        //            item = addObjManager.objs[index];
        //        }
        //    }
        //}
    }

    void Start()
    {
        if (StageManager.getInstance())
        {
            stageManager = StageManager.getInstance();
        }
        else
        {
            print("ClickItem : Stage Manager is Missing.");
        }

        if (gameObject.GetComponent<UIButton>())
        {
            uiButton = gameObject.GetComponent<UIButton>();
        }
        else
        {
            print("ClickItem : Can't get UIButton");
        }

        //refresh();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void refresh()
    {
        nItemLabel.text = item.numOfObj.ToString();     //아이템 갯수 표시 업데이트
    }

    public void refresh(bool _isEnabled)
    {
        if (uiButton)
        {
            uiButton.isEnabled = _isEnabled;                //아이템 생성 버튼을 비활성화.
            nItemLabel.text = item.numOfObj.ToString();     //아이템 갯수 표시 업데이트
        }
    }

    public IEnumerator makeObject()
    {
        //오브젝트의 transform 초기화
        Transform parentTrans = GameObject.Find("Objects").transform;
        Quaternion initRot = Quaternion.Euler(item.initRot);
        Vector3 initPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, -Camera.main.transform.position.z));

        GameObject newObj = (GameObject)Instantiate(item.prefab, initPos, initRot);
        stageManager.addedObjects.Add(newObj);
        newObj.GetComponent<MchObject>().isMoved = true;    //움직일 수 있는 물체이다
        newObj.GetComponent<MchObject>().isPicked = true;   //현재 선택된 상태
        Global_Variable.curCtrlMode = ControlMode.CONTROL_MODE_PLAY;    //Play모드로 변경
        Global_Variable.curMode = TransformMode.MODE_TRANSLATION;   //움직이기 모드로 변경

        yield return null;
    }

    public virtual void Click()
    {
        if(Global_Variable.curCtrlMode == ControlMode.CONTROL_MODE_CAMERA)
        {
            if (item.numOfObj > 0)  //현재 가져올 수 있는 오브젝트가 갯수가 1개 이상일 경우
            {
                item.numOfObj--;    //현재 아이템 갯수 감소

                StartCoroutine(this.makeObject());  //아이템 생성

                if(item.numOfObj <= 0 && uiButton)       //생성 뒤 아이템 갯수가 0 이하이면
                {
                    refresh(false);
                }
                else
                {
                    refresh();
                }
            }
        }
    }
}