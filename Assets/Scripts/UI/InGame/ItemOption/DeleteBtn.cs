using UnityEngine;
using System.Collections;

public class DeleteBtn : ItemOptionBtn {

    private AddObjectManager addObjManager;

    public override void Start()
    {
        base.Start();

        if(FindObjectOfType<AddObjectManager>())
        {
            addObjManager = FindObjectOfType<AddObjectManager>();
        }
        else
        {
            print("DeleteBtn : Can't find AddObjectManager");
        } 
    }

    void OnMouseDown()
    {
        foreach (playObject obj in addObjManager.objs)
        {
            if (attachedMchObj.name == string.Format("{0}(Clone)", obj.prefab.name))
            {
                obj.numOfObj++;
                foreach (ClickItem item in GameObject.FindObjectsOfType<ClickItem>())
                {
                    if (item.type == obj.prefab.GetComponent<MchObject>().type)
                    {
                        print("DeleteBtn : UIButton Enabled");
                        item.refresh(true);
                        break;
                    }
                }

                break;
            }
        }
        Destroy(attachedMchObj);
        Destroy(transform.parent.gameObject);
    }
}
