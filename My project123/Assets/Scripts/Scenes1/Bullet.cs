using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg;
    private int hp;
    public bool isRotate;

     void OnEnable()
    {
        hp = 1;    
    }
    void Update()
    {
        if (isRotate)
            transform.Rotate(Vector3.forward*10); //2D���ӿ��� ȸ���� Z����


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "BorderBullet")
            {
                gameObject.SetActive(false);
            }

        if (gameObject.tag == "SpecialBullet")
        {
            if (collision.gameObject.tag == "Enemy")
            { 
                hp--;
                if (hp < 0)
                {
                    gameObject.SetActive(false);
                }

            }
        }

        
    }
}

