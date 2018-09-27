using UnityEngine;
using System.Collections;

public class DeleteUserWindow : PopUpClass {

    private int _deleteIndex;
    private SelectPlayerBtn _callBtn;

    public int deleteIndex
    {
        get
        {
            return _deleteIndex;
        }
        set
        {
            _deleteIndex = value;
        }
    }
    public SelectPlayerBtn callBtn
    {
        get
        {
            return _callBtn;
        }
        set
        {
            _callBtn = value;
        }
    }

    public void noBtn()
    {
        offPopUp();
    }

    public void yesBtn()
    {
        if(PlayerPrefs.HasKey("nPlayer" + _deleteIndex.ToString()) &&
            PlayerPrefs.HasKey("playerName" + _deleteIndex.ToString()) &&
            PlayerPrefs.HasKey("nClearStage" + _deleteIndex.ToString()))
        {
            PlayerPrefs.DeleteKey("nPlayer" + _deleteIndex.ToString());
            PlayerPrefs.DeleteKey("playerName" + _deleteIndex.ToString());
            PlayerPrefs.DeleteKey("nClearStage" + _deleteIndex.ToString());
            _callBtn.update();
            gameObject.SetActive(false);
        }
        else
        {
            print("DeleteUserWindow : Can't delete account");
        }
    }
}
