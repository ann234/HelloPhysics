using UnityEngine;
using System.Collections;

public class Pipe : MchObject {

    protected override void initColliders()
    {
        NumOfCollision = 0;

        foreach(Collider cold in GetComponentsInChildren<Collider>())
        {
            if(cold.tag != "PipeCollider")
            {
                cols.Add(cold);
            }
        }
        foreach(MeshCollider col in cols)
        {
            ((MeshCollider)col).convex = true;
            col.isTrigger = true;
        }
    }

    protected override void setColTrigger(bool isOn)
    {
        if (isOn)
        {
            foreach (MeshCollider col in cols)
            {
                ((MeshCollider)col).convex = true;
                col.isTrigger = true;
            }
        }
        else
        {
            foreach (MeshCollider col in cols)
            {
                col.isTrigger = false;
                ((MeshCollider)col).convex = false;
            }
        }
    }

    #region Collision Trigger functions
    /// <summary>
    void OnTriggerEnter(Collider other)
    {
        if (isMoved && other.gameObject.tag != "PipeCollider")
        {
            Global_Variable.collideObj++;
            numOfCollision++;
            if (isPicked || (Global_Variable.curMode == TransformMode.MODE_ROTATION))
            {
                denySprite.SetActive(true); //show inhibition image
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "PipeCollider")
        {
            isCollided = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isCollided = false;
        if (isMoved && other.gameObject.tag != "PipeCollider")
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

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
