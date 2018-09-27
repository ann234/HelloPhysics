using UnityEngine;
using System.Collections;

public class SelectUserWindow : WindowClass {

    private PlayerInfo[] players = new PlayerInfo[3];

	void newGame()
    {

    }

    public override void Awake()
    {
        base.Awake();
        for(int i = 0; i < 3; i++)
        {
            PlayerInfo pInfo = new PlayerInfo();
            if (PlayerPrefs.HasKey("nPlayer" + i))
            {
                pInfo.nPlayer = PlayerPrefs.GetInt("nPlayer" + i);
                pInfo.playerName = PlayerPrefs.GetString("playerName" + i);
                pInfo.nClearStage = PlayerPrefs.GetInt("nClearStage" + i);
            }
            else
            {
                pInfo.nPlayer = -1;
                pInfo.playerName = "";
                pInfo.nClearStage = 0;
            }
            players[i] = pInfo;
        }
    }
}
