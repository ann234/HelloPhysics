using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class WinCondition02 : WinCondition {

    public override void Awake()
    {
        base.Awake();
    }

    void OnCollisionEnter(Collision cols)
    {
        if ( cols.gameObject.tag == "Iron" )
        {
            afterWin();
        }
    }
}
