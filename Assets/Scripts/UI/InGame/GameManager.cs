using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private StageManager stageManager;

    public ClearWindow clearWindow;
    public GameObject option;
    public float gravityScale = -15.0f;

    public UILabel startButtonLabel;
   
    public List<ClickItem> clickItems;

    /**Window that explain Stage's goal*/
    [SerializeField]
    private GameObject missionWindow;
    [SerializeField]
    private GameObject tutorialWindow;

    public GameObject getMissionWindow()
    {
        return missionWindow;
    }

    public void openOption()
    {
        option.SetActive( !option.activeSelf );
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void gotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void goNextStage()
    {
        stageManager.changeStage(stageManager.curStage + 1);
        clearWindow.closeWindow();
        startButtonLabel.text = "Start";
    }

    public void retryStage()
    {
        stageManager.resetStage();
        clearWindow.closeWindow();
        startButtonLabel.text = "Start";
    }

    void Awake()
    {
        if (StageManager.getInstance())
            stageManager = StageManager.getInstance();
        else
            print("GameManager : Can't get stageManager");

        Physics.gravity = new Vector3(0, gravityScale, 0);

        clickItems.AddRange(GameObject.FindObjectsOfType<ClickItem>());
    }

    void Start()
    {
        if(tutorialWindow)
        {
            if(Global_Variable.curPlayer.nClearStage <= 0)
            {
                tutorialWindow.GetComponent<PopUpClass>().onPopUp();
            }
        }
        else
        {
            print("GameManager : can't find TutorialWindow");
        }
    }
}