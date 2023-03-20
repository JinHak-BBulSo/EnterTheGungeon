using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Prefabs_Enemy")]
    [SerializeField] public GameObject bossBulletKingPrefab = default;
    [Header("Prefabs_Bullet")]
    [SerializeField] public GameObject bulletBasicPrefab = default;
    [SerializeField] public GameObject bulletTypeAPrefab = default;
    [SerializeField] public GameObject bulletTypeBPrefab = default;

    GameObject[] enemyBulletKing = default;
    GameObject[] bulletBasic = default;
    GameObject[] bulletTypeA = default;
    GameObject[] bulletTypeB = default;

    GameObject[] targetPool = default;

    //  [YHJ] 2023-03-18
    //  @brief Awake
    private void Awake()
    {
        enemyBulletKing = new GameObject[1];
        bulletBasic = new GameObject[500];
        bulletTypeA = new GameObject[500];
        bulletTypeB = new GameObject[500];

        Generate();
    }   //  Awake()

    //  [YHJ] 2023-03-18
    //  @brief ObjectPooling / Instantiate
    private void Generate()
    {
        //  Enemy
        for (int index = 0; index < enemyBulletKing.Length; index++)
        {
            enemyBulletKing[index] = Instantiate(bossBulletKingPrefab);
            enemyBulletKing[index].SetActive(false);
        }

        //  Bullet
        //  Bullet_Basic : Basic
        for (int index = 0; index < bulletBasic.Length; index++)
        {
            bulletBasic[index] = Instantiate(bulletBasicPrefab);
            bulletBasic[index].SetActive(false);
        }

        //  BulletTypeA : Rotation
        for (int index = 0; index < bulletTypeA.Length; index++)
        {
            bulletTypeA[index] = Instantiate(bulletTypeAPrefab);
            bulletTypeA[index].SetActive(false);
        }

        //  bulletTypeB : Flicker
        for (int index = 0; index < bulletTypeB.Length; index++)
        {
            bulletTypeB[index] = Instantiate(bulletTypeBPrefab);
            bulletTypeB[index].SetActive(false);
        }
    }   //  Generate()

    //  [YHJ] 2023-03-18
    //  @brief ObjectPooling / SetActive
    public GameObject MakeObject(string type_)
    {
        switch (type_)
        {
            case "Bullet_Basic":
                targetPool = bulletBasic;
                break;
            case "Bullet_TypeA":
                targetPool = bulletTypeA;
                break;
            case "Bullet_TypeB":
                targetPool = bulletTypeB;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    //  [YHJ] 2023-03-18
    //  @brief ObjectPooling
    public GameObject[] GetPool(string type_)
    {
        switch (type_)
        {
            case "Bullet_Basic":
                targetPool = bulletBasic;
                break;
            case "Bullet_TypeA":
                targetPool = bulletTypeA;
                break;
            case "Bullet_TypeB":
                targetPool = bulletTypeB;
                break;
        }

        return targetPool;
    }
}
