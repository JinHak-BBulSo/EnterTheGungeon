using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [Header("Prefabs_Enemy")]
    [SerializeField ]public GameObject bossBulletKingPrefab = default;
    [Header("Prefabs_Bullet")]
    [SerializeField]public GameObject bulletPrefab = default;

    GameObject[] enemyBullet = default;
    GameObject[] enemyBulletKing = default;

    GameObject[] targetPool = default;

    //  [YHJ] 2023-03-18
    //  @brief Awake
    private void Awake()
    {
        enemyBulletKing = new GameObject[1];
        enemyBullet = new GameObject[200];

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
        for (int index = 0; index < enemyBullet.Length; index++)
        {
            enemyBullet[index] = Instantiate(bulletPrefab);
            enemyBullet[index].SetActive(false);
        }
    }   //  Generate()

    //  [YHJ] 2023-03-18
    //  @brief ObjectPooling / SetActive
    public GameObject MackObject(string type_)
    {
        switch (type_)
        {
            case "EnemyBullet":
                targetPool = enemyBullet;
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
            case "EnemyBullet":
                targetPool = enemyBullet;
                break;
        }

        return targetPool;
    }
}
