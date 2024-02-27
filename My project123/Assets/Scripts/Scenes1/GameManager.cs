using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using static ItemData;

public class GameManager : MonoBehaviour
{
    public Slider xpSlider;

    public ItemData[] Card;

    public int playerLevel;
    public int bigEnemy;


    public int stage;
    public Animator StageAnim;
    public Animator ClearAnim;
    public Animator FadeAnim;
    public Transform playerPos;

    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;
    public float spawnUpTime;


    public GameObject player;
    public Text scoreText;
    public GameObject gameOverSet;


    public ObjectManager objectManager;
    public CardManager cardManager;

    public GameObject levelUpUi;


    public int spawnIndex;
    public bool spawnEnd;


    public bool spawnBoss;

    private int enemyPoint;

    void Awake()
    {


        enemyObjs = new string[] { "EnemyS", "EnemyM", "EnemyL", "EnemyB" };
        StageStart();
        ActivePlayer();
        CardReset();
    }
    void Update()
    {
        spawnUpTime += Time.deltaTime;
        curSpawnDelay += Time.deltaTime;
        LevelUp();
        if (curSpawnDelay > nextSpawnDelay)
        {
            SpawnEnemy();

            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

    }

    void LevelUp()
    {

        if (xpSlider.value >= 1)
        {
            Time.timeScale = 0;
            playerLevel++;
            
            cardManager.CardSet();
            levelUpUi.SetActive(true);


            xpSlider.value = 0;
        }



    }
    public void DamageUp()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.power++;

        levelUpUi.SetActive(false);
        Time.timeScale = 1;
    }
    public void SpeedUp()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.speed++;

        levelUpUi.SetActive(false);
        Time.timeScale = 1;
    }
    public void HpUp()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.life++;
        
        playerLogic.hpSprite.size += new Vector2(0.1f, 0f);
        playerLogic.hpSprite.transform.position += (Vector3.right * 0.035f);
        levelUpUi.SetActive(false);
        Time.timeScale = 1;
    }
    public void ShootSpeedUp()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.maxShotDelay -= 0.08f;

        levelUpUi.SetActive(false);
        Time.timeScale = 1;
    }
    public void AddShoot()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.shoot++;

        levelUpUi.SetActive(false);
        Time.timeScale = 1;
    }
    public void ExpUp()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerLogic.exp += 0.1f;

        levelUpUi.SetActive(false);
        Time.timeScale = 1;
    }


    void ActivePlayer()
    {

        player.SetActive(true);
    }
    void CardReset()
    {
        for (int i = 0; i < Card.Length; i++)
        {
            Card[i].skillLevel = 0;
        }
    }


    public void StageStart()
    {
        //Stage UI Load
        StageAnim.SetTrigger("On");
        StageAnim.GetComponent<Text>().text = "Stage" + stage + "\nStart";
        StageAnim.GetComponent<Text>().text = "Stage" + stage + "\nClear";
        //Enemy Spawn File Read
        // ReadSpawnFile();


        //Fade In
        FadeAnim.SetTrigger("In");
    }
    public void StageEnd()
    {
        //Clear UI Load
        ClearAnim.SetTrigger("On");

        //Fade Out
        FadeAnim.SetTrigger("Out");
        //Player Repos
        player.transform.position = playerPos.position;
        //Stage Increament
        stage++;
        if (stage > 1)
        {
            GameOver();
        }
        else
            Invoke("StageStart", 5);

    }


    void SpawnEnemy()
    {
        if (spawnBoss)
        {
            return;
        }

        int enemyIndex = 0;
        string sEnemy = "S";
        //초반엔S만 나오게 시간이지날수록 M,L "같이"생성
        // 초반에 어느정도 잡다가 B생성
        // 임시 S도 나오되 M과 L은 S보다 빈도가 작아야함.
        if (bigEnemy % 10 == 0)
        {
            sEnemy = "M";

        }
        if (bigEnemy % 20 == 0)
        {
            sEnemy = "L";

        }
        if (bigEnemy % 30 == 0)
        {
            sEnemy = "B";

            spawnBoss = true;
        }
        bigEnemy++;
        

        switch (sEnemy)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "M":
                enemyIndex = 1;
                break;
            case "L":
                enemyIndex = 2;
                break;
            case "B":
                enemyIndex = 3;
                break;

        }



        List<int> EnemyList = new List<int>();
        int currentNumber = Random.Range(0, 8);

        for (int i = 0; i < 3;)
        {
            if (EnemyList.Contains(currentNumber))
            {
                currentNumber = Random.Range(0, 8);
            }
            else
            {
                EnemyList.Add(currentNumber);
                i++;
            }
        }

        for (int i = 0; i < 3; i++)
        {

            if (enemyIndex == 3)
            {
                enemyPoint = 2;
            }
            else
            {
                enemyPoint = EnemyList[i];
            }
        

        GameObject enemy = objectManager.Makeobj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.gameObject.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;

        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }
        if (enemyIndex == 3)
        {
            break;
        }
    }
        //시간이 갈수록 딜레이 감소
        if (spawnUpTime > 15)
        {
            spawnUpTime = 0;
            if (nextSpawnDelay > 1)
            {
                nextSpawnDelay -= 0.04f;
            }
            else
            {
                nextSpawnDelay = 1;
            }

        }

    }
   
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
        
    }
    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.Makeobj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }



    void RespawnPlayerExe()
    {
            player.transform.position = Vector3.down * 3.5f;
            player.SetActive(true);
            Player playerLogic = player.GetComponent<Player>();
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);

    }


}
