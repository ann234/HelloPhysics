using UnityEngine;
using System.Collections;

public class MakeUserWindow : PopUpClass {

    private ScrollWindow scrollVeiw;
    private SelectPlayerBtn _btn;
    private int _index;

    public PopUpClass popUpNullWindow;
    public PopUpClass popUpOverlapWindow;

    public SelectPlayerBtn btn
    {
        get
        {
            return _btn;
        }
        set
        {
            _btn = value;
        }
    }
    public int index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
        }
    }
    public PlayerInfo newPlayer;
    public UIInput inputField;

    public void confirm()
    {
        string pName = inputField.value;

        if (pName == "")
        {
            popUpNullWindow.onPopUp();
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                if (PlayerPrefs.GetString("playerName" + i).Equals(pName))
                {
                    popUpOverlapWindow.onPopUp();
                    return;
                }
            }

            newPlayer.playerName = pName;
            newPlayer.nPlayer = index;
            newPlayer.nClearStage = 0;
            PlayerPrefs.SetInt("nPlayer" + index.ToString(), index);
            PlayerPrefs.SetString("playerName" + index.ToString(), pName);
            PlayerPrefs.SetInt("nClearStage" + index.ToString(), 0);

            _btn.isAvailable = true;
            _btn.update();

            offPopUp();   //turn off MakeUserWindow
            scrollVeiw.onPopUp();
        }
        inputField.value = "";
        inputField.LoadValue();    //reset input field
    }


    public void cancle()
    {
        inputField.value = "";
        inputField.LoadValue();    //reset input field
    }

    void Awake()
    {
        if (!FindObjectOfType<ScrollWindow>())
        {
            print("Can't find ScrollWindow");
        }
        else
        {
            scrollVeiw = FindObjectOfType<ScrollWindow>();
        }

        newPlayer = new PlayerInfo();
    }
}
