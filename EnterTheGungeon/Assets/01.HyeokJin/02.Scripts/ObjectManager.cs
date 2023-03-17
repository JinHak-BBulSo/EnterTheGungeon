using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab = default;

    GameObject[] bullet = default;
    GameObject[] targetPool = default;

    private void Awake()
    {
        bullet = new GameObject[100];

        Generate();
    }

    private void Generate()
    {
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = Instantiate(bulletPrefab);
            bullet[i].SetActive(false);
        }
    }

    private GameObject MakeObj(string type_)
    {
        switch (type_)
        {
            case "bullet":
                targetPool = bullet;
                break;
        }

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }
}
