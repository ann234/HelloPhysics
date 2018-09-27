using UnityEngine;
using System.Collections;

public class MakeUser : MonoBehaviour {

    private GameObject popUpWindow;
    private SelectPlayerBtn _btn;
    private int _index;

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

        if(pName == "")
        {
            popUpWindow.SetActive(true);
        }
        else
        {
            newPlayer.playerName = pName;
            newPlayer.nPlayer = index;
            newPlayer.nClearStage = 0;
            PlayerPrefs.SetInt("nPlayer" + index.ToString(), index);
            PlayerPrefs.SetString("playerName" + index.ToString(), pName);
            PlayerPrefs.SetInt("nClearStage" + index.ToString(), 0);

            _btn.isAvailable = true;
            _btn.update();

            transform.parent.gameObject.SetActive(false);   //turn off MakeUserWindow
        }
        inputField.LoadValue();    //reset input field
    }

    void Awake()
    {
        newPlayer = new PlayerInfo();

        popUpWindow = FindObjectOfType<PopUpClass>().gameObject;
        popUpWindow.SetActive(false);
    }
}
