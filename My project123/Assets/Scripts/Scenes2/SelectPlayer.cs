using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour
{
    public static bool selectplayer; // True�� �Ѿ� False�� ������ ĳ����
    public void SelectPlayer1()
    {
        selectplayer = true;
        Debug.Log("�÷��̾�1 ���� "+selectplayer);
    }
    public void SelcetPlayer2()
    {

        selectplayer = false;
        
        Debug.Log("�÷��̾�2 ���� "+selectplayer);
        
    }
    public bool GetBool()
    {
        return selectplayer;
    }
   

}
