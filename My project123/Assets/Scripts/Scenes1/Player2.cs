using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Player2 : MonoBehaviour
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
    public float power;
    public int boom;
    public int Maxboom;
    public float maxPower;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameManager gameManager;
    public ObjectManager objectManager;


    public bool isBoomTime;

    //public GameObject[] followers;
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



        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1) /*|| !isControl*/)
            h = 0;
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1) /*|| !isControl*/)
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
        
        if (skilTime >= 1.5f)
        {
            GameObject bullet = objectManager.Makeobj("LazerPlayerS1");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            curShotDelay = 0;
        }
        


    }
    

    public void ButtonADown()
    {
        skilTime = 0;
        

    }
   



    public void Fire()
    {
        


        if (curShotDelay < maxShotDelay)
        {
            return;
        }

        switch (power)
        {
            case 1:
                GameObject bullet = objectManager.Makeobj("LazerPlayerA");
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            case 2:
                GameObject bulletR = objectManager.Makeobj("LazerPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;

                GameObject bulletL = objectManager.Makeobj("LazerPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                maxShotDelay = 0.15f;
                break;
            default:
                GameObject bulletRR = objectManager.Makeobj("LazerPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.35f;
                GameObject bulletLL = objectManager.Makeobj("LazerPlayerA");
                bulletLL.transform.position = transform.position + Vector3.left * 0.35f;

                GameObject bulletCC = objectManager.Makeobj("LazerPlayerA"); //큰 총알
                bulletCC.transform.position = transform.position;
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                maxShotDelay = 0.1f;
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
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (power == maxPower)
                        score += 500;
                    else
                    {
                        power++;
                        //AddFollower();
                    }
                    break;

                case "Boom":
                    if (boom == Maxboom)
                        score += 500;
                    break;

            }
            collision.gameObject.SetActive(false);

        }
        else if (collision.gameObject.tag == "Exp")
        {
            gameManager.xpSlider.value += 0.2f;

            collision.gameObject.SetActive(false);
        }



    }


    /*
    void AddFollower()
    {
        if (power == 4)
            followers[0].SetActive(true);
        else if (power == 5)
            followers[1].SetActive(true);
        else if (power == 6)
            followers[2].SetActive(true);
    }
    */





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
