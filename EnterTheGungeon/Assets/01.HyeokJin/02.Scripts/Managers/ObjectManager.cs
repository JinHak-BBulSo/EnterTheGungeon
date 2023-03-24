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
    [SerializeField] public GameObject bulletTypeCPrefab = default;
    [SerializeField] public GameObject bulletTypeDPrefab = default;
    [SerializeField] public GameObject bulletTypeEPrefab = default;
    [SerializeField] public GameObject bulletTypeFPrefab = default;

    GameObject[] bulletBasic = default;
    GameObject[] bulletTypeA = default;
    GameObject[] bulletTypeB = default;
    GameObject[] bulletTypeC = default;
    GameObject[] bulletTypeD = default;
    GameObject[] bulletTypeE = default;
    GameObject[] bulletTypeF = default;

    GameObject[] targetPool = default;

    //  [YHJ] 2023-03-18
    //  @brief Awake
    private void Awake()
    {
        bulletBasic = new GameObject[500];
        bulletTypeA = new GameObject[500];
        bulletTypeB = new GameObject[500];
        bulletTypeC = new GameObject[10];
        bulletTypeD = new GameObject[10];
        bulletTypeE = new GameObject[50];
        bulletTypeF = new GameObject[500];

        Generate();
    }   //  Awake()

    //  [YHJ] 2023-03-18
    //  @brief ObjectPooling / Instantiate
    private void Generate()
    {
        //  Enemy


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

        //  bulletTypeC : Big
        for (int index = 0; index < bulletTypeC.Length; index++)
        {
            bulletTypeC[index] = Instantiate(bulletTypeCPrefab);
            bulletTypeC[index].SetActive(false);
        }

        //  bulletTypeD : Grenade
        for (int index = 0; index < bulletTypeD.Length; index++)
        {
            bulletTypeD[index] = Instantiate(bulletTypeDPrefab);
            bulletTypeD[index].SetActive(false);
        }

        //  bulletTypeE : Long
        for (int index = 0; index < bulletTypeE.Length; index++)
        {
            bulletTypeE[index] = Instantiate(bulletTypeEPrefab);
            bulletTypeE[index].SetActive(false);
        }

        //  bulletTypeF : Bounce
        for (int index = 0; index < bulletTypeF.Length; index++)
        {
            bulletTypeF[index] = Instantiate(bulletTypeFPrefab);
            bulletTypeF[index].SetActive(false);
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
            case "Bullet_TypeC":
                targetPool = bulletTypeC;
                break;
            case "Bullet_TypeD":
                targetPool = bulletTypeD;
                break;
            case "Bullet_TypeE":
                targetPool = bulletTypeE;
                break;
            case "Bullet_TypeF":
                targetPool = bulletTypeF;
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
    private GameObject[] GetPool(string type_)
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
            case "Bullet_TypeC":
                targetPool = bulletTypeC;
                break;
            case "Bullet_TypeD":
                targetPool = bulletTypeD;
                break;
            case "Bullet_TypeE":
                targetPool = bulletTypeE;
                break;
            case "Bullet_TypeF":
                targetPool = bulletTypeF;
                break;
        }

        return targetPool;
    }
}