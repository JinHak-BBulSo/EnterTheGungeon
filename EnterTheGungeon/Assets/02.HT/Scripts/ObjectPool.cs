using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /*public List<GameObject> pathFinderGridPool;
    public GameObject pathFinderGridPrefab;*/
    int pathFinderGridPoolSize;

    public List<GameObject> enemyBulletPool = new List<GameObject>();
    public GameObject enemyBulletPrefab = default;
    int enemyBulletPoolSize;

    public List<GameObject> poisonAreaPool = new List<GameObject>();
    public GameObject poisonAreaPrefab = default;
    int poisonAreaPoolSize;

    private void Awake()
    {
        transform.parent = PlayerManager.Instance.player.transform.parent.parent;

    }
    void Start()
    {
        /*pathFinderGridPool = new List<GameObject>();
        pathFinderGridPrefab = Resources.Load<GameObject>("02.HT/Prefabs/PathFinder/PathFinderGrid");
        pathFinderGridPoolSize = 0;

        for (int i = 0; i < pathFinderGridPoolSize; i++)
        {
            GameObject clone_ = Instantiate(pathFinderGridPrefab, transform.GetChild(0));
            clone_.SetActive(false);
            pathFinderGridPool.Add(clone_);
        }*/

        enemyBulletPrefab = Resources.Load<GameObject>("02.HT/Prefabs/TestBullet");
        enemyBulletPoolSize = 500;

        poisonAreaPrefab = Resources.Load<GameObject>("02.HT/Prefabs/BossGorgun/PoisonArea");
        poisonAreaPoolSize = 500;

        for (int i = 0; i < enemyBulletPoolSize; i++)
        {
            GameObject bulletClone_ = Instantiate(enemyBulletPrefab, transform.GetChild(1));
            enemyBulletPool.Add(bulletClone_);
            bulletClone_.SetActive(false);

            GameObject poisonClone_ = Instantiate(poisonAreaPrefab, transform.GetChild(2));
            poisonAreaPool.Add(poisonClone_);
            poisonClone_.SetActive(false);
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
