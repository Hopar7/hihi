using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;


    // Start is called before the first frame update
    void Awake()
    {
        anim=gameObject.GetComponent<Animator>();
    }
    void OnEnable()
    {
        Invoke("Disable", 0.5f);    
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    public void StartExplosion(string target)
    {
        anim.SetTrigger("OnExplosion");

        switch (target)
        {
            case "S":
                transform.localScale = Vector3.one * 0.7f;
                break;
            case "M":
                transform.localScale = Vector3.one * 1f;
                break;
            case "P":
                transform.localScale = Vector3.one * 0.5f;
                break;
            case "L":
                transform.localScale = Vector3.one * 2f;
                break;
            case "B":
                transform.localScale = Vector3.one * 3f;
                break;

        }

    }
}
