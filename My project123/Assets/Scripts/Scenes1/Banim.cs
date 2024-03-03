using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banim : MonoBehaviour
{
    Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>(); 
    }

    public void setAnimDown()
    {
        anim.SetBool("isDown", true);
    }

    public void setAnimUp()
    {
        anim.SetBool("isDown", false);
    }
}
