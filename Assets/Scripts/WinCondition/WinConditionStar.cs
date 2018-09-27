using UnityEngine;
using System.Collections;

public class WinConditionStar : WinCondition {

    private int nStars;

    public override void Start()
    {
        base.Start();
        //nStars = GameObject.FindGameObjectsWithTag("Star").Length;
        //print("Start nStars : " + nStars);
    }

    virtual public void getStar()
    {
        nStars = GameObject.FindGameObjectsWithTag("Star").Length;
        if(nStars - 1 <= 0)
        {
            afterWin();
        }
        //nStars -= 1;
        //print(nStars);
        //if(nStars <= 0)
        //{
        //    afterWin();
        //}
    }
}
