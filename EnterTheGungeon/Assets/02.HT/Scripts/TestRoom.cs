using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoom : MonoBehaviour
{
    public List<string> enemy;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        //EnemyManager.Instance.CreateEnemy(enemy[i], this.transform);
        //EnemyManager.Instance.CreateBoss(boss, this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.Instance.isTest)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                EnemyManager.Instance.CreateEnemy(enemy[i], this.transform);
            }
            EnemyManager.Instance.isTest = false;
        }

        if (EnemyManager.Instance.isBossTest)
        {
            EnemyManager.Instance.CreateBoss(boss, this.transform);
            EnemyManager.Instance.isBossTest = false;
        }
    }
}
