using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    public GameObject enemyBPrefab;
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;


    public GameObject LazerPlayerAPrefab;
    public GameObject LazerPlayerBPrefab;

    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;

    public GameObject explosionPrefab;

    public GameObject expPrefab;
    public GameObject exp2Prefab;
    public GameObject exp3Prefab;

    GameObject[] enemyB;
    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;



    GameObject[] LazerPlayerA;
    GameObject[] LazerPlayerB;

    GameObject[] BulletEnemyA;
    GameObject[] BulletEnemyB;
    GameObject[] BulletBossA;
    GameObject[] BulletBossB;
    GameObject[] explosion;

    GameObject[] Exp;
    GameObject[] Exp2;
    GameObject[] Exp3;

    GameObject[] targerPool;

    void Awake()
    {
        enemyB = new GameObject[5];
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];

        LazerPlayerA = new GameObject[300];
        LazerPlayerB = new GameObject[300];

        BulletEnemyA = new GameObject[100];
        BulletEnemyB = new GameObject[100];
        BulletBossA = new GameObject[50];
        BulletBossB = new GameObject[1000];

        explosion = new GameObject[30];

        Exp= new GameObject[100];
        Exp2 = new GameObject[100];
        Exp3 = new GameObject[100];

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
        for (int i = 0; i < LazerPlayerA.Length; i++)
        {
            LazerPlayerA[i] = Instantiate(LazerPlayerAPrefab);
            LazerPlayerA[i].SetActive(false);
        }
      
        for (int i = 0; i < LazerPlayerB.Length; i++)
        {
            LazerPlayerB[i] = Instantiate(LazerPlayerBPrefab);
            LazerPlayerB[i].SetActive(false);
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
        for (int i = 0; i < Exp.Length; i++)
        {
            Exp[i] = Instantiate(expPrefab);
            Exp[i].SetActive(false);
        }
        for (int i = 0; i < Exp2.Length; i++)
        {
            Exp2[i] = Instantiate(exp2Prefab);
            Exp2[i].SetActive(false);
        }
        for (int i = 0; i < Exp3.Length; i++)
        {
            Exp3[i] = Instantiate(exp3Prefab);
            Exp3[i].SetActive(false);
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
            case "LazerPlayerA":
                targerPool = LazerPlayerA;
                break;
            case "LazerPlayerB":
                targerPool = LazerPlayerB;
                break;
            case "BulletEnemyA":
                targerPool = BulletEnemyA;
                break;
            case "BulletEnemyB":
                targerPool = BulletEnemyB;
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
            case "Exp2":
                targerPool = Exp2;
                break;
            case "Exp3":
                targerPool = Exp3;
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
            case "LazerPlayerA":
                targerPool = LazerPlayerA;
                break;
            case "LazerPlayerB":
                targerPool = LazerPlayerB;
                break;
            case "BulletEnemyA":
                targerPool = BulletEnemyA;
                break;
            case "BulletEnemyB":
                targerPool = BulletEnemyB;
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
            case "Exp2":
                targerPool = Exp2;
                break;
            case "Exp3":
                targerPool = Exp3;
                break;
        }


        return targerPool;
    }

}
