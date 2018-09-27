using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenStage : MonoBehaviour {

    private StageManager stageManager;
    private GameObject lockSprite;

    public int index = 0;

    public void openStage()
    {
        stageManager.curStage = index;
        SceneManager.LoadScene("GameScene");
    }

    void Awake()
    {
        lockSprite = GetComponentInChildren<UISprite>().gameObject;
    }

	// Use this for initialization
	void Start () {
        if (StageManager.getInstance())
            stageManager = StageManager.getInstance();
        else
            print("OpenStage : Can't get stageManager.");
	}

    public void refresh(bool isStageCleared)
    {
        //first, set false all button and true lock sprite
        GetComponent<UIButton>().enabled = false;
        lockSprite.SetActive(true);

        //and if user clear this stage
        if (isStageCleared)
        {
            //unlock this stage
            GetComponent<UIButton>().enabled = true;
            lockSprite.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}