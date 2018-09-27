using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ControlMode
{
    CONTROL_MODE_CAMERA,    //카메라 이동 모드
    CONTROL_MODE_PLAY       //오브젝트 플레이 모드
}

public enum TransformMode
{
    MODE_NOTHING,
    MODE_TRANSLATION,
    MODE_ROTATION
}

/**Item's type*/
public enum ItemType
{
    NULL,
    PIPE_STRAIGHT,
    PIPE_SPIN,
    BOWLING,
    BILLARDBALL,
    BOX,
    BOOK,
    FOOTBALL,
    BOARD_SPIN,
    BOARD
}

public class Global_Variable : MonoBehaviour {

    private static Global_Variable instance;

    public static float bgmVolume;
    
    public static ControlMode curCtrlMode;
    public static TransformMode curMode;
    public static int collideObj;
    public static bool isSimulate;
    public static bool isLogin;
    public static PlayerInfo curPlayer;
    public static bool offtarget = false;

    public static void resetVar()
    {
        curCtrlMode = ControlMode.CONTROL_MODE_CAMERA;
        curMode = TransformMode.MODE_TRANSLATION;
        collideObj = 0;
        isSimulate = false;
    }

    public static Global_Variable getInstance()
    {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<Global_Variable>();
        }
        return instance;
    }

    void Awake()
    {
        bgmVolume = 1.0f;
    }

	// Use this for initialization
	void Start () {
        curCtrlMode = ControlMode.CONTROL_MODE_CAMERA;
        curMode = TransformMode.MODE_TRANSLATION;
        collideObj = 0;
        isSimulate = false;

        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.G))
        {

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            curMode = TransformMode.MODE_TRANSLATION;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            curMode = TransformMode.MODE_ROTATION;
        }
    }

    //Global Functions
    public static IEnumerator moveObject(Transform srcTrans, Transform destTrans)
    {
        while ((srcTrans.position - destTrans.position).magnitude > 0.1f)
        {
            srcTrans.position = Vector3.Lerp(srcTrans.position, destTrans.position, 0.2f);
            yield return null;
        }
    }

    public static IEnumerator moveObject(Vector3 srcTrans, Vector3 destTrans)
    {
        while ((srcTrans - destTrans).magnitude > 0.1f)
        {
            srcTrans = Vector3.Lerp(srcTrans, destTrans, 0.2f);
            yield return null;
        }
    }
}

public struct PlayerInfo
{
    public int nPlayer;
    public string playerName;
    public int nClearStage;

    public PlayerInfo(int _nPlayer, string _playerName, int _nClearStage)
    {
        this.nPlayer = _nPlayer;
        this.playerName = _playerName;
        this.nClearStage = _nClearStage;
    }

    public void saveInfo()
    {
        PlayerPrefs.SetInt("nClearStage" + nPlayer.ToString(), nClearStage);
    }
}

[System.Serializable]
public abstract class ButtonIntf : MchObject
{
   abstract public void switchOn();
}

