using UnityEngine;
using System.Collections;

public class LogoutBtn : MonoBehaviour {

    public void logout()
    {
        Global_Variable.isLogin = false;
    }
}
