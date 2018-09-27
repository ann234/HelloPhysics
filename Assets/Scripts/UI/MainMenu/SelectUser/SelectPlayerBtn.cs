using UnityEngine;
using System.Collections;

public class SelectPlayerBtn : MonoBehaviour {
    private PlayerInfo player;
    private bool _isAvailable = false;
    private ScrollWindow scrollVeiw;
    private MakeUserWindow makeUserWindow;
    private DeleteUserWindow deleteUserWindow;

    private SelectUserWindow selectUserWindow;
    private StartWindow startWindow;

    private UIPlayTween[] tweener;

    public bool isAvailable
    {
        get
        {
            return _isAvailable;
        }
        set
        {
            _isAvailable = value;
        }
    }
    public int index;
    public UILabel label_nStage;
    public UILabel label_userName;

    public void logIn()
    {
        if (_isAvailable)
        {
            Global_Variable.curPlayer = player;
            Global_Variable.isLogin = true;
            //스테이지 선택 화면으로 이동
            tweener[0].Play(true);
            tweener[1].Play(true);

            //Open stages that user completed
            int count = Global_Variable.curPlayer.nClearStage;
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("StageButton"))
            {
                //if stage is cleared
                obj.GetComponent<OpenStage>().refresh(count >= 0);

                count -= 1;
            }
        }
        else
        {
            scrollVeiw.offPopUp();
            makeUserWindow.onPopUp();
            makeUserWindow.newPlayer = player;
            makeUserWindow.index = index;
            makeUserWindow.btn = GetComponent<SelectPlayerBtn>();
        }
    }

    public void deleteAccount()
    {
        if(_isAvailable)
        {
            deleteUserWindow.onPopUp();
            scrollVeiw.offPopUp();

            if (!deleteUserWindow.GetComponent<DeleteUserWindow>())
            {
                print("SelectPlayerBtn : Can't find DeleteUserWindow");
            }
            else
            {
                DeleteUserWindow deleteUser = deleteUserWindow.GetComponent<DeleteUserWindow>();
                deleteUser.deleteIndex = index;
                deleteUser.callBtn = GetComponent<SelectPlayerBtn>();
            }
        }
    }

    public void update()
    {
        if (PlayerPrefs.HasKey("nPlayer" + index))
        {
            player.nPlayer = PlayerPrefs.GetInt("nPlayer" + index);
            player.playerName = PlayerPrefs.GetString("playerName" + index);
            player.nClearStage = PlayerPrefs.GetInt("nClearStage" + index);
            _isAvailable = true;
        }
        else
        {
            player.nPlayer = index;
            player.playerName = "Make New User";
            player.nClearStage = 0;
            _isAvailable = false;
        }

        label_userName.text = player.playerName;
        label_nStage.text =
            string.Format("Stage : {0} / 8", player.nClearStage);
    }

    void Awake()
    {
        if (!GameObject.FindObjectOfType<MakeUserWindow>())
        {
            print("Can't find MakeUserWindow");
        }
        else
        {
            makeUserWindow = FindObjectOfType<MakeUserWindow>();
        }

        if (!FindObjectOfType<DeleteUserWindow>())
        {
            print("Can't find DeleteUserWindow");
        }
        else
        {
            deleteUserWindow = FindObjectOfType<DeleteUserWindow>();
        }

        if (!FindObjectOfType<ScrollWindow>())
        {
            print("Can't find ScrollWindow");
        }
        else
        {
            scrollVeiw = FindObjectOfType<ScrollWindow>();
        }

        tweener = new UIPlayTween[2];
        int count = 0;
        foreach(UIPlayTween tw in GetComponents<UIPlayTween>())
        {
            tweener[count] = tw;
            count++;
        }

        update();
    }

    void Start()
    {
        makeUserWindow.offPopUp();
        deleteUserWindow.offPopUp();

        label_userName.text = player.playerName;
        label_nStage.text =
            string.Format("Stage : {0} / 8", player.nClearStage);
        //label_nStage.text = 
        //    string.Format("Stage : {0} / {1}", player.nClearStage, StageManager.getMaxStage());

        update();
    }

}