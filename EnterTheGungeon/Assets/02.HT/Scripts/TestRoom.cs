using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoom : MonoBehaviour
{
    public List<string> enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.Instance.isTest)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                EnemyManager.Instance.CreateEnemy(enemy[i]);
            }
            EnemyManager.Instance.isTest = false;
        }
    }
}
