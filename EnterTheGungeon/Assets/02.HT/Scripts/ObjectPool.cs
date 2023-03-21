using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pathFinderGridPool;
    public GameObject pathFinderGridPrefab;
    int pathFinderGridPoolSize;

    public List<GameObject> enemyBulletPool;
    public GameObject enemyBulletPrefab;
    int enemyBulletPoolSize;

    public List<GameObject> poisonAreaPool;
    public GameObject poisonAreaPrefab;
    int poisonAreaPoolSize;

    void Start()
    {
        pathFinderGridPool = new List<GameObject>();
        pathFinderGridPrefab = Resources.Load<GameObject>("02.HT/Prefabs/PathFinder/PathFinderGrid");
        pathFinderGridPoolSize = 2000;

        for (int i = 0; i < pathFinderGridPoolSize; i++)
        {
            GameObject clone_ = Instantiate(pathFinderGridPrefab, transform.GetChild(0));
            clone_.SetActive(false);
            pathFinderGridPool.Add(clone_);
        }


        enemyBulletPool = new List<GameObject>();
        enemyBulletPrefab = Resources.Load<GameObject>("02.HT/Prefabs/TestBullet");
        enemyBulletPoolSize = 500;

        for (int i = 0; i < enemyBulletPoolSize; i++)
        {
            GameObject clone_ = Instantiate(enemyBulletPrefab, transform.GetChild(1));
            //clone_.SetActive(false);
            enemyBulletPool.Add(clone_);
        }

        poisonAreaPool = new List<GameObject>();
        poisonAreaPrefab = Resources.Load<GameObject>("02.HT/Prefabs/BossGorgun/PoisonArea");
        poisonAreaPoolSize = 500;

        for (int i = 0; i < poisonAreaPoolSize; i++)
        {
            GameObject clone_ = Instantiate(poisonAreaPrefab, transform.GetChild(2));
            //clone_.SetActive(false);
            poisonAreaPool.Add(clone_);
        }
    }

    // @brief poolName_: pathFinderGridPool, enemyBulletPool, poisonAreaPool
    // @brief prefabName_ : pathFinderGridPrefab, enemyBulletPrefab, poisonAreaPrefab
    // @brief poolNumber_ : 0, 1, 2
    public GameObject GetObject(List<GameObject> poolName_, GameObject prefabName_, int poolNumber_)
    {
        for (int i = 0; i < poolName_.Count; i++)
        {
            if (!poolName_[i].activeInHierarchy)
            {
                //poolName_[i].transform.SetParent(transform_);
                poolName_[i].SetActive(true);
                return poolName_[i];
            }
        }

        GameObject clone_ = Instantiate(prefabName_, transform.GetChild(poolNumber_));
        clone_.SetActive(true);
        poolName_.Add(clone_);

        return clone_;
    }

    public void ReturnObject(GameObject clone_, int poolNumber_)
    {
        clone_.SetActive(false);
        if (clone_.transform.parent != this.transform.GetChild(poolNumber_))
        {
            clone_.transform.SetParent(this.transform.GetChild(poolNumber_));
        }
    }
}
