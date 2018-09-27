using UnityEngine;
using System.Collections;
using System;

public class WinCondition01 : WinCondition
{
    public float goalForce = 0.5f;

    public override void Awake()
    {
        base.Awake();
    }

    void OnCollisionEnter(Collision cols)
    {
        float force = cols.relativeVelocity.magnitude;
        if (force > goalForce)
        {
            afterWin();
            //SceneManager.LoadScene("Scene02");
        }
    }
}
