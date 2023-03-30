using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyBook : MonoBehaviour
{
    public List<GameObject> enemyList;
    public List<GameObject> bossList;
    Transform enemyListLine1;
    Transform enemyListLine2;
    Transform bossListLine1;
    Transform bossListLine2;

    public List<string> enemyName;
    public Dictionary<string, bool> enemyFindCheck;
    public List<string> bossName;
    public Dictionary<string, bool> bossFindCheck;


    // { Var for EnemyInfo
    Transform enemyInfo;
    public TMP_Text textEnemyName;
    public Image imageEnemyInfo;
    public TMP_Text textEnemyExplain1;
    public TMP_Text textEnemyExplain2;
    public TMP_Text textEnemyLongDesc;


    // } Var for EnemyInfo
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


        enemyInfo = transform.parent.parent.GetChild(1).GetChild(1);
        textEnemyName = enemyInfo.GetChild(0).GetComponent<TMP_Text>();
        imageEnemyInfo =  enemyInfo.GetChild(2).GetComponent<Image>();
        textEnemyExplain1 = enemyInfo.GetChild(3).GetChild(0).GetComponent<TMP_Text>();
        textEnemyExplain2 = enemyInfo.GetChild(4).GetChild(0).GetComponent<TMP_Text>();
        textEnemyLongDesc = enemyInfo.GetChild(7).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
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
