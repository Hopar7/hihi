using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;
    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject LazerPlayerAPrefab;
    public GameObject bulletPlayerS1Prefab;
    public GameObject LazerPlayerS1Prefab;

    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletFollowerPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;
    public GameObject explosionPrefab;

    public GameObject expPrefab;

    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] BulletPlayerA;
    GameObject[] BulletPlayerB;
    GameObject[] LazerPlayerA;
    GameObject[] BulletPlayerS1;
    GameObject[] LazerPlayerS1;



    GameObject[] BulletEnemyA;
    GameObject[] BulletEnemyB;
    GameObject[] BulletFollower;
    GameObject[] BulletBossA;
    GameObject[] BulletBossB;
    GameObject[] explosion;

    GameObject[] Exp;

    GameObject[] targerPool;

    void Awake()
    {
        enemyB = new GameObject[5];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        itemCoin= new GameObject[20];
        itemPower= new GameObject[10];
        itemBoom= new GameObject[10];

        BulletPlayerA = new GameObject[100];
        BulletPlayerB = new GameObject[100];
        LazerPlayerA = new GameObject[100];
        BulletPlayerS1 = new GameObject[10];
        LazerPlayerS1 = new GameObject[10];

        BulletEnemyA = new GameObject[100];
        BulletEnemyB = new GameObject[100];
        BulletFollower = new GameObject[100];
        BulletBossA = new GameObject[50];
        BulletBossB = new GameObject[1000];
        explosion = new GameObject[20];

        Exp= new GameObject[100];
        Generate();
    }
    void Generate()
    {
        for (int i = 0; i < enemyB.Length; i++)
        {
            enemyB[i] = Instantiate(enemyBPrefab);
            enemyB[i].SetActive(false);
        }
        for (int i=0;i<enemyL.Length;i++)
        {
            enemyL[i]= Instantiate(enemyLPrefab);
            enemyL[i].SetActive(false);
        }
        for (int i = 0; i < enemyM.Length; i++)
        {
            enemyM[i] = Instantiate(enemyMPrefab);
            enemyM[i].SetActive(false);
        }
        for (int i = 0; i < enemyS.Length; i++)
        {
            enemyS[i] = Instantiate(enemySPrefab);
            enemyS[i].SetActive(false);

        }
        for (int i = 0; i < itemCoin.Length; i++)
        {
            itemCoin[i] = Instantiate(itemCoinPrefab);
            itemCoin[i].SetActive(false);
        }
        for (int i = 0; i < itemPower.Length; i++)
        {
            itemPower[i] = Instantiate(itemPowerPrefab);
            itemPower[i].SetActive(false);
        }
        for (int i = 0; i < itemBoom.Length; i++)
        {
            itemBoom[i] = Instantiate(itemBoomPrefab);
            itemBoom[i].SetActive(false);
        }
        for (int i = 0; i < BulletPlayerA.Length; i++)
        {
            BulletPlayerA[i] = Instantiate(bulletPlayerAPrefab);
            BulletPlayerA[i].SetActive(false);
        }
        for (int i = 0; i < BulletPlayerB.Length; i++)
        {
            BulletPlayerB[i] = Instantiate(bulletPlayerBPrefab);
            BulletPlayerB[i].SetActive(false);
        }
        for (int i = 0; i < LazerPlayerA.Length; i++)
        {
            LazerPlayerA[i] = Instantiate(LazerPlayerAPrefab);
            LazerPlayerA[i].SetActive(false);
        }
        for (int i = 0; i < BulletPlayerS1.Length; i++)
        {
            BulletPlayerS1[i] = Instantiate(bulletPlayerS1Prefab);
            BulletPlayerS1[i].SetActive(false);
        }
        for (int i = 0; i < LazerPlayerS1.Length; i++)
        {
            LazerPlayerS1[i] = Instantiate(LazerPlayerS1Prefab);
            LazerPlayerS1[i].SetActive(false);
        }

        for (int i = 0; i < BulletEnemyA.Length; i++)
        {
            BulletEnemyA[i] = Instantiate(bulletEnemyAPrefab);
            BulletEnemyA[i].SetActive(false);
        }
        for (int i = 0; i < BulletEnemyB.Length; i++)
        {
            BulletEnemyB[i] = Instantiate(bulletEnemyBPrefab);
            BulletEnemyB[i].SetActive(false);
        }
        for(int i=0;i<BulletFollower.Length;i++)
        {
            BulletFollower[i] = Instantiate(bulletFollowerPrefab);
            BulletFollower[i].SetActive(false);
        }
        for (int i = 0; i < BulletBossA.Length; i++)
        {
            BulletBossA[i] = Instantiate(bulletBossAPrefab);
            BulletBossA[i].SetActive(false);
        }
        for (int i = 0; i < BulletBossB.Length; i++)
        {
            BulletBossB[i] = Instantiate(bulletBossBPrefab);
            BulletBossB[i].SetActive(false);
        }
        for (int i = 0; i < explosion.Length; i++)
        {
            explosion[i] = Instantiate(explosionPrefab);
            explosion[i].SetActive(false);
        }
        for(int i = 0; i <Exp.Length; i++)
        {
            Exp[i] = Instantiate (expPrefab);
            Exp[i].SetActive(false);
        }

    }
    public GameObject Makeobj(string type)
    {

        switch (type)
        {
            case "EnemyB":
                targerPool = enemyB;
                break;
            case "EnemyL":
                targerPool = enemyL;
                break;
            case "EnemyM":
                targerPool = enemyM;
                break;
            case "EnemyS":
                targerPool = enemyS;
                break;
            case "ItemCoin":
                targerPool = itemCoin;
                break;
            case "ItemPower":
                targerPool = itemPower;
                break;
            case "ItemBoom":
                targerPool = itemBoom;
                break;
            case "BulletPlayerA":
                targerPool = BulletPlayerA;
                break;
            case "BulletPlayerB":
                targerPool = BulletPlayerB;
                break;
            case "LazerPlayerA":
                targerPool = LazerPlayerA;
                break;
            case "BulletPlayerS1":
                targerPool = BulletPlayerS1;
                break;
            case "LazerPlayerS1":
                targerPool = LazerPlayerS1;
                break;
            case "BulletEnemyA":
                targerPool = BulletEnemyA;
                break;
            case "BulletEnemyB":
                targerPool = BulletEnemyB;
                break;
            case "BulletFollow":
                targerPool = BulletFollower;
                break;
            case "BulletBossA":
                targerPool = BulletBossA;
                break;
            case "BulletBossB":
                targerPool = BulletBossB;
                break;
            case "Explosion":
                targerPool = explosion;
                break;
            case "Exp":
                targerPool = Exp;
                break;

        }
        for (int i = 0; i < targerPool.Length; i++)
        {
            if (!targerPool[i].activeSelf)
            {
                targerPool[i].SetActive(true);
                return targerPool[i];
            }

        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyB":
                targerPool = enemyB;
                break;
            case "EnemyL":
                targerPool = enemyL;
                break;
            case "EnemyM":
                targerPool = enemyM;
                break;
            case "EnemyS":
                targerPool = enemyS;
                break;
            case "ItemCoin":
                targerPool = itemCoin;
                break;
            case "itemPower":
                targerPool = itemPower;
                break;
            case "itemBoom":
                targerPool = itemBoom;
                break;
            case "BulletPlayerA":
                targerPool = BulletPlayerA;
                break;
            case "BulletPlayerB":
                targerPool = BulletPlayerB;
                break;
            case "LazerPlayerA":
                targerPool = LazerPlayerA;
                break;
            case "BulletPlayerS1":
                targerPool = BulletPlayerS1;
                break;
            case "LazerPlayerS1":
                targerPool = LazerPlayerS1;
                break;
            case "BulletEnemyA":
                targerPool = BulletEnemyA;
                break;
            case "BulletEnemyB":
                targerPool = BulletEnemyB;
                break;
            case "BulletFollow":
                targerPool = BulletFollower;
                break;
            case "BulletBossA":
                targerPool = BulletBossA;
                break;
            case "BulletBossB":
                targerPool = BulletBossB;
                break;
           case "Explosion":
              targerPool = explosion;
                break;
            case "Exp":
                targerPool = Exp;
                break;
        }


        return targerPool;
    }

}