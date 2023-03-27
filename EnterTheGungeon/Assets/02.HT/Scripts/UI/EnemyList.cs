using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<GameObject> enemyList;
    public List<GameObject> bossList;
    Transform enemyListLine1;
    Transform enemyListLine2;
    Transform bossListLine1;
    Transform bossListLine2;

    List<string> enemyName;
    Dictionary<string, bool> enemyFindCheck;
    List<string> bossName;
    Dictionary<string, bool> bossFindCheck;
    // Start is called before the first frame update
    void Start()
    {
        enemyName = EnemyManager.Instance.enemyName;
        enemyFindCheck = EnemyManager.Instance.enemyFindCheck;

        bossName = EnemyManager.Instance.enemyName;
        bossFindCheck = EnemyManager.Instance.bossFindCheck;

        enemyListLine1 = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0);
        enemyListLine2 = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1);
        for (int i = 0; i < enemyListLine1.childCount; i++)
        {
            enemyList.Add(enemyListLine1.GetChild(i).gameObject);
        }
        for (int i = 0; i < enemyListLine2.childCount; i++)
        {
            enemyList.Add(enemyListLine2.GetChild(i).gameObject);
        }

        bossListLine1 = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(4);
        bossListLine2 = transform.GetChild(2).GetChild(0).GetChild(0).GetChild(5);
        for (int i = 0; i < bossListLine1.childCount; i++)
        {
            bossList.Add(bossListLine1.GetChild(i).gameObject);
        }
        for (int i = 0; i < bossListLine2.childCount; i++)
        {
            bossList.Add(bossListLine2.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (i < enemyName.Count && enemyName[i] != null)
            {
                if (enemyFindCheck[enemyName[i]])
                {
                    enemyList[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    enemyList[i].transform.GetChild(0).gameObject.SetActive(false);
                }

            }
        }
        
        /* 
        for (int i = 0; i < bossList.Count; i++)
        {
            if (i < bossName.Count && bossName[i] != null)
            {
                if (bossFindCheck[enemyName[i]])
                {
                    bossList[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    bossList[i].transform.GetChild(0).gameObject.SetActive(false);
                }

            }
        } */
    }
}
