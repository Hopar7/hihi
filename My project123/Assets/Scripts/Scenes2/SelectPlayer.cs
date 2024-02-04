using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public static bool selectplayer; // True면 총알 False면 레이저 캐릭터
    public void SelectPlayer1()
    {
        selectplayer = true;
        Debug.Log("플레이어1 시작 "+selectplayer);
    }
    public void SelcetPlayer2()
    {

        selectplayer = false;
        
        Debug.Log("플레이어2 시작 "+selectplayer);
        
    }
    public bool GetBool()
    {
        return selectplayer;
    }
   

}
