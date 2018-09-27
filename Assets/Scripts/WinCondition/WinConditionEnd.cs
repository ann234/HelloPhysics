using UnityEngine;
using System.Collections;

public class WinConditionEnd : WinConditionStar {

    private int nStars;
    private AudioSource audioSrc;

    void Awake()
    {
        
        audioSrc = GetComponent<AudioSource>();
    }

    public override void getStar()
    {
        nStars = GameObject.FindGameObjectsWithTag("Star").Length;
        if (nStars - 1 <= 0)
        {
            StartCoroutine(this.lastWin());
        }
        //nStars -= 1;
        //print(nStars);
        //if(nStars <= 0)
        //{
        //    afterWin();
        //}
    }

    IEnumerator lastWin()
    {
        yield return new WaitForSeconds(2);
        audioSrc.Play();
    }
}
