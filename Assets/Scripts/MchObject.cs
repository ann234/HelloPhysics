using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MchObject : MonoBehaviour {

    private StageManager stageManager;
    private AddObjectManager addObjManager;
    protected GameObject denySprite;  //image when collilsion occured
    protected uint numOfCollision;    //number of object that collide with this
    protected bool isCollided = false;    //check if it is collided with other

    //오브젝트 움직임을 위해
    private Vector3 scrSpace, offset, curScreenSpace;
    private Quaternion initRotation;
    protected List<Collider> cols = new List<Collider>();
    protected Rigidbody rb;
    private ItemOption itemOption; //rotation, delete button
    
    public bool isMoved = false;
    public bool isPicked = false;

    /**Image for Item UI*/
    //public Texture itemImage;

    /**Item type*/
    public ItemType type = ItemType.NULL;

    #region Getter and setter
    public uint NumOfCollision
    {
        get
        {
            return numOfCollision;
        }
        set
        {
            numOfCollision = value;
        }
    }
    #endregion

    protected virtual void initColliders()
    {
        numOfCollision = 0;

        cols.Add(GetComponent<Collider>());
        foreach (var item in cols)
        {
            item.isTrigger = true;
        }
    }

    protected virtual void setColTrigger(bool isOn)
    {
        if(isOn)
        {
            foreach(var item in cols)
            {
                item.isTrigger = true;
            }
        }
        else
        {
            foreach (var item in cols)
            {
                item.isTrigger = false;
            }
        }
    }

    //destructor
    void OnDestroy()
    {
        if(denySprite)
        {
            if (denySprite.gameObject)
            {
                Destroy(denySprite.gameObject);
            }
            if (itemOption)
            {
                if (itemOption.gameObject)
                {
                    Destroy(itemOption.gameObject);
                }
            }
        }
    }

    public void Simulation(bool isSimulate)
    {
        if (isSimulate) //Simulation Start
        {
            rb.useGravity = true;
            setColTrigger(false);
        }
        else
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            setColTrigger(true);
        }
    }

    public virtual void doSound(float volume)
    {

    }

    // Use this for initialization
    public virtual void Start()
    {
        initColliders();
        stageManager = StageManager.getInstance();
        addObjManager = AddObjectManager.getInstance();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        
        //Attach X sprite and ItemOption UI on MchObject
        denySprite = (GameObject)Instantiate(Resources.Load("DenySprite", typeof(GameObject)), Vector3.zero, Quaternion.identity);
        denySprite.tag = "MchObjectUI";
        denySprite.SetActive(false);
        itemOption = ((GameObject)Instantiate(Resources.Load("itemOption", typeof(GameObject)), Vector3.zero, Quaternion.identity)).GetComponent<ItemOption>();
        itemOption.parentObj = gameObject;
        itemOption.tag = "MchObjectUI";
        itemOption.gameObject.SetActive(false);

        initRotation = transform.rotation;

        //transform setup
        scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, scrSpace.z));

        curScreenSpace = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, scrSpace.z));
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(!Global_Variable.isSimulate)
        {
            //  update deny image's position
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, -1);
            denySprite.transform.position = pos;
            itemOption.transform.position = pos;

            if(Input.GetKeyDown(KeyCode.D))
            {
                deleteObject();
            }

            if (Global_Variable.curCtrlMode == ControlMode.CONTROL_MODE_PLAY && isPicked)
            {
                StartCoroutine(this.transformObject());
            }
        }
    }

    #region Collision Trigger functions
    /// <summary>
    void OnTriggerEnter(Collider other)
    {
        if (isMoved)
        {
            Global_Variable.collideObj++;
            numOfCollision++;
            if(isPicked || (Global_Variable.curMode == TransformMode.MODE_ROTATION))
            {
                denySprite.SetActive(true); //show inhibition image
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        isCollided = true;
    }

    void OnTriggerExit(Collider other)
    {
        isCollided = false;
        if (isMoved)
        {
            Global_Variable.collideObj--;
            numOfCollision--;
            if (numOfCollision == 0)
            {
                denySprite.SetActive(false);
            }
        }
    }
    /// </summary>
    #endregion Collision Trigger functions

    #region Mouse transform interface
    /// <summary>
    void deleteObject()
    {
        if(isPicked && isMoved)
        {
            print("Input : D");
            foreach(playObject pObj in addObjManager.objs)  //추가 아이템 리스트들을 돌면서
            {
                if(this.gameObject.GetType() == pObj.prefab.GetType())  //삭제하려는 오브젝트와 pObj의 프리팹이 같은 경우
                {
                    Global_Variable.curCtrlMode = ControlMode.CONTROL_MODE_CAMERA; 
                    Destroy(this.gameObject);   //삭제
                    pObj.numOfObj++;            //갯수 원상 복귀
                    print("Destroy Picked Object");
                    return;
                }
            }
        }
    }

    //for right click object
    void OnMouseOver()
    {
        if(!Global_Variable.isSimulate && Input.GetMouseButtonUp(1))   //right click
        {
            if (isMoved && !isCollided && (transform.position.y > 0))
            {
                foreach(ItemOption item in FindObjectsOfType<ItemOption>())
                {
                    if(item.gameObject.activeSelf == true)
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                itemOption.gameObject.SetActive( !itemOption.gameObject.activeSelf );
                Global_Variable.curMode = TransformMode.MODE_ROTATION;

                isPicked = false;
            }
        }
    }
    
    //for left click
    void OnMouseDown()
    {
        if (isMoved && !isCollided && (transform.position.y > 0))
        //  현재 오브젝트가 플레이 가능한 오브젝트이고
        //  다른 오브젝트와 충돌하지 않았으며
        //  y축으로 0보다 위에 있으면(UI부분으로 갔을 경우에 놓아지지 않도록 하기 위해)
        {
            if (isPicked)    //선택되어있던 오브젝트를 떼는 경우
            {
                Global_Variable.curCtrlMode = ControlMode.CONTROL_MODE_CAMERA;  //카메라모드로 전환
                Global_Variable.offtarget = true;
            } 
            else //오브젝트를 선택한 경우
            {
                Global_Variable.curCtrlMode = ControlMode.CONTROL_MODE_PLAY;  //플레이 모드로 전환
                Global_Variable.curMode = TransformMode.MODE_TRANSLATION;

                //turn off itemOption
                foreach (ItemOption item in FindObjectsOfType<ItemOption>())
                {
                    if (item.gameObject.activeSelf == true)
                    {
                        item.gameObject.SetActive(false);
                    }
                }
                itemOption.gameObject.SetActive(false);
                Global_Variable.offtarget = false;
            }
            isPicked = !isPicked;

            scrSpace = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, scrSpace.z));
        }
    }

    IEnumerator transformObject()
    {
        //if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0)) //if mouse cursor move
        {
            Vector3 curScreenSpace = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, scrSpace.z));

            if (Global_Variable.curMode == TransformMode.MODE_TRANSLATION)
            {
                //translation
                Vector3 curPosition = curScreenSpace + offset;
                transform.position = curPosition;
            }

            //if (Global_Variable.curMode == TransformMode.MODE_TRANSLATION)
            //{
            //    //translation
            //    Vector3 curPosition = curScreenSpace + offset;
            //    transform.position = curPosition;
            //}
            //else if (Global_Variable.curMode == TransformMode.MODE_ROTATION)
            //{
            //    //rotation
            //    float theta = (180f / Mathf.PI) * Mathf.Atan2(curScreenSpace.y - transform.position.y,
            //        curScreenSpace.x - transform.position.x);

            //    Vector3 angle = transform.rotation.eulerAngles;
            //    transform.rotation = Quaternion.Euler(0, 0, theta) * initRotation;
            //}
        }

        yield return null;
    }

    /// </summary>
    #endregion Mouse transform interface
}