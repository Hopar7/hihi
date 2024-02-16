using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float MaxShotDelay;
    public float curShotDelay;

    
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;
    SpriteRenderer spriteRenderer;

    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == "B")
            anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "B":
                health = 500;
                Invoke("Stop", 2);
                break;
            case "L":
                health = 130;
                break;
            case "M":
                health = 40;
                break;
            case "S":
                health = 10;
                break;
        }
    }
    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;
        switch (patternIndex)
        {
            case 0:
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }

    void FireFoward()
    {
        if (health <= 0) return;
        GameObject bulletR = objectManager.Makeobj("BulletBossA");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.Makeobj("BulletBossA");
        bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
        GameObject bulletL = objectManager.Makeobj("BulletBossA");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.Makeobj("BulletBossA");
        bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);




        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);
        else
            Invoke("Think", 3);

    }
    void FireShot()
    {
        if (health <= 0) return;
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = objectManager.Makeobj("BulletEnemyB");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec;

       
                dirVec = player.transform.position - transform.position;
            
      
            


            Vector2 ranVec = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

        }

        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }
    void FireArc()
    {
        if (health <= 0) return;
        GameObject bullet = objectManager.Makeobj("BulletEnemyA");

        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 6, ForceMode2D.Impulse);




        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.15f);
        else
            Invoke("Think", 3);
    }
    void FireAround()
    {
        if (health <= 0) return;
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;
        for (int i = 0; i < roundNum; i++)
        {
            GameObject bullet = objectManager.Makeobj("BulletBossB");

            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum)
                                       , Mathf.Sin(Mathf.PI * 2 * i / roundNum));


            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);

        }


        curPatternCount++;
        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }



    void Update()
    {
        if (enemyName == "B")
            return;

        Fire();

        Reload();

    }

    void Fire()
    {

        if (curShotDelay < MaxShotDelay)
        {
            return;
        }

        if (enemyName == "S")
        {
            GameObject bullet = objectManager.Makeobj("BulletEnemyA");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 driVec;
            driVec = player.transform.position - transform.position;
            rigid.AddForce(driVec.normalized * 4, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            GameObject bulletR = objectManager.Makeobj("BulletEnemyB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = objectManager.Makeobj("BulletEnemyB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 driVecR;
            Vector3 driVecL;
            
            driVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            driVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
           
            rigidR.AddForce(driVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(driVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    public void OnHit(int dmg)
    {
        if (health <= 0)
            return;



        health -= dmg;
        if (enemyName == "B")
        {
            anim.SetTrigger("OnHit");
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
            Invoke("ReturnSprite", 0.05f);

        }
        if (health <= 0)
        {
            Player playerLogic = player.GetComponent<Player>(); 
            playerLogic.score += enemyScore;

            //Random ratio Item Drop
            int ran = enemyName == "B" ? 0 : UnityEngine.Random.Range(0, 10);
            if (ran < 4) //40%
            {
               
                GameObject exp = objectManager.Makeobj("Exp");
                exp.transform.position = transform.position;
                Debug.Log("EXP");
            }
            else if (ran < 7) //30%
            {
                GameObject exp = objectManager.Makeobj("Exp");
                exp.transform.position = transform.position;

                Debug.Log("EXP");
            }
            else if (ran < 9) //20%
            {
                GameObject exp = objectManager.Makeobj("Exp");
                exp.transform.position = transform.position;

                Debug.Log("EXP");
            }
            else if (ran < 10) //10%
            {
                GameObject exp = objectManager.Makeobj("Exp");
                exp.transform.position = transform.position;

                Debug.Log("EXP");
            }



            //CancelInvoke();
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            gameManager.CallExplosion(transform.position, enemyName);


            if(enemyName == "B")
            {
                gameManager.StageEnd();
            }

        }
    }

    void ReturnSprite()
    { 
        spriteRenderer.sprite = sprites[0];
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="BorderBullet" && enemyName !="B")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
        else if(collision.gameObject.tag=="PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            collision.gameObject.SetActive(false);
            Player playerLogic = player.GetComponent<Player>();

            OnHit(playerLogic.power);
        }
        else if(collision.gameObject.tag == "SpecialBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
        }
    }
}
