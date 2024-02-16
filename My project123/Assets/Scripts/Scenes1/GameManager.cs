using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Slider xpSlider;

    public ItemData[] Card;


    public int stage;
    public Animator StageAnim;
    public Animator ClearAnim;
    public Animator FadeAnim;
    public Transform playerPos;

    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public GameObject gameOverSet;


    public ObjectManager objectManager;
    public CardManager cardManager;

    public GameObject levelUpUi;


    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

 



    void Awake()
    {
        spawnList =new List<Spawn>();

       enemyObjs = new string[] {"EnemyS","EnemyM","EnemyL", "EnemyB" };
        StageStart();
        ActivePlayer();
        CardReset();
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        LevelUp();
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
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
        for(int i=0;i<Card.Length;i++)
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
        ReadSpawnFile();


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
        if(stage >1)
        {
            GameOver();
        }
        else
            Invoke("StageStart", 5);

    }



    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);
        
        while(stringReader != null)
        {

            string line = stringReader.ReadLine();
           // Debug.Log(line);
            if (line == null)
                break;


            Spawn spawnDate = new Spawn();
            spawnDate.delay = float.Parse(line.Split(',')[0]);
            spawnDate.type = line.Split(',')[1];
            spawnDate.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnDate);
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }



  

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
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


        
        int enemyPoint = spawnList[spawnIndex].point;
        GameObject enemy = objectManager.Makeobj(enemyObjs[enemyIndex]);
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.gameObject.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        enemyLogic.player = player;
        enemyLogic.gameManager = this;
        enemyLogic.objectManager = objectManager;

        if(enemyPoint == 5 || enemyPoint == 6 )
        {
            enemy.transform.Rotate(Vector3.back*90);
            rigid.velocity = new Vector2(enemyLogic.speed*(-1),-1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else
        {
            rigid.velocity = new Vector2 (0,enemyLogic.speed*(-1));
        }

        //¸®½ºÆù
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
        

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
