using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float skilTime;

    public SpriteRenderer hpSprite;

    public bool isTouchTop;
    public bool isTouchBottom;  
    public bool isTouchRight;
    public bool isTouchLeft;

    public int life;
    public int score;

    public float speed;
    public int power;
    public int shoot;
    public float exp;

    public float maxPower;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameManager gameManager;
    public ObjectManager objectManager;

    public bool isRespawnTime;

    public bool[] JoyControl;
    public bool isControl;
    public bool isButtonA;
    

    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        Unbeatable();
        Invoke("Unbeatable", 3);
    }

    void Unbeatable()
    {
        isRespawnTime = !isRespawnTime;

        if (isRespawnTime)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }



    void Update()
    {
        skilTime += Time.deltaTime;
        Move();
        Fire();
        Reload();
        



    }
    public void JoyPanel(int type)
    {
        for (int i = 0; i < 9; i++)
        {
            JoyControl[i] = i == type;
        }
    }

    public void JoyDown()
    {
        isControl = true;
    }
    public void JoyUp()
    {
        isControl = false;
    }




    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");  //수평동작 키 키보드
        
        float v = Input.GetAxisRaw("Vertical");  //수직동작 키 키보드

        //if (JoyControl[0]) { h = -1; v = 1; }
        //if (JoyControl[1]) { h = 0; v = 1; }
        //if (JoyControl[2]) { h = 1; v = 1; }
        //if (JoyControl[3]) { h = -1; v = 0; }
        //if (JoyControl[4]) { h = 0; v = 0; }
        //if (JoyControl[5]) { h = 1; v = 0; }
        //if (JoyControl[6]) { h = -1; v = -1; }
        //if (JoyControl[7]) { h = 0; v = -1; }
        //if (JoyControl[8]) { h = 1; v = -1; }



        if ((h == 1 && isTouchRight) || (h == -1 && isTouchLeft) /*|| !isControl*/)
            h = 0;
        if ((v == 1 && isTouchTop) || (v == -1 && isTouchBottom) /*|| !isControl*/)
            v = 0;

        Vector3 curPos = transform.position;  // 처음위치
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;  //이동할 위치  

        transform.position = curPos + nextPos;  //위치변경 


        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }
   
    public void ButtonAUP()
    {
        isButtonA = false;



    }


    public void ButtonADown()
    {
        isButtonA = true;


    }
   

    void ShootRightAdd(float position)
    {
        GameObject bullet = objectManager.Makeobj("LazerPlayerA");
        bullet.transform.position = transform.position + Vector3.right *position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        
    }
    void ShootLeftAdd(float position)
    {
        GameObject bullet = objectManager.Makeobj("LazerPlayerA");
        bullet.transform.position = transform.position + Vector3.left * position;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

    }


    public void Fire()
    {
        if(isButtonA==false)
        {
            return;
        }


        if (curShotDelay < maxShotDelay)
        {
            return;
        }


        switch (shoot)
        {
            case 1:
                ShootRightAdd(0);
               
                break;
            case 2:
                ShootRightAdd(0.05f);
                ShootLeftAdd(0.05f);
                
                break;
            case 3:
                ShootRightAdd(0.1f);
                ShootRightAdd(0);
                ShootLeftAdd(0.1f);
                
                break;
            case 4:
                ShootRightAdd(0.15f);
                ShootRightAdd(0.05f);
                ShootLeftAdd(0.05f);
                ShootLeftAdd(0.15f);
                break;
            case 5:
                ShootRightAdd(0.2f);
                ShootRightAdd(0.1f);
                ShootRightAdd(0);
                ShootLeftAdd(0.1f);
                ShootLeftAdd(0.2f);
                break;
            case 6:
                ShootRightAdd(0.25f);
                ShootRightAdd(0.15f);
                ShootRightAdd(0.05f);
                ShootLeftAdd(0.05f);
                ShootLeftAdd(0.15f);
                ShootLeftAdd(0.25f);
                //레벨이 7이되면은 3발로 줄어드는대신 1회관통 가능으로??
                break;
        }
        curShotDelay = 0;

    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            life--;
            hpSprite.size -= new Vector2(0.1f, 0f);
            hpSprite.transform.position += (Vector3.left * 0.035f);

            gameManager.CallExplosion(transform.position, "P");

            if (life == 0)
            {
                gameManager.GameOver();

                gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "Exp")
        {
            gameManager.xpSlider.value += exp;

            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Exp2")
        {
            gameManager.xpSlider.value += exp +0.2f;

            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Exp3")
        {
            gameManager.xpSlider.value += exp +0.4f;

            collision.gameObject.SetActive(false);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;

            }


        }
    }


}
