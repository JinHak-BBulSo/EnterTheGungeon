using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyListButton : MonoBehaviour
{
    EnemyBook enemyBook;
    List<GameObject> enemyList;
    List<GameObject> bossList;

    string defaultTextEnemyName;
    Sprite[] AmmonomiconSprite;
    Sprite defaultImageEnemyInfo;
    string defaultTextEnemyExplain1;
    string defaultTextEnemyExplain2;
    string textEnemyLongDesc;

    // Start is called before the first frame update
    void Start()
    {
        enemyBook = transform.parent.parent.parent.parent.parent.GetComponent<EnemyBook>();
        enemyList = enemyBook.enemyList;
        bossList = enemyBook.bossList;

        defaultTextEnemyName = "Unknown";

        AmmonomiconSprite = Resources.LoadAll<Sprite>("02.HT/Sprites/Ammonomicon/EnemyBook/Ammonomicon");
        defaultImageEnemyInfo = AmmonomiconSprite[69];

        defaultTextEnemyExplain1 = "???";
        defaultTextEnemyExplain2 = "???";
        textEnemyLongDesc = "Some secret of gungeon is still veiled";
    }

    // Update is called once per frame
    void Update()
    {

        //enemyList.IndexOf(this.gameObject)

    }

    public void SetEnemyInfo()
    {
        int index_ = 0;
        /* enemyList.IndexOf(this.gameObject);
        bossList.IndexOf(this.gameObject) */

        if (enemyList.IndexOf(this.gameObject) != -1)
        {
            index_ = enemyList.IndexOf(this.gameObject);
        }

        if (bossList.IndexOf(this.gameObject) != -1)
        {
            index_ = bossList.IndexOf(this.gameObject);
        }



        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            enemyBook.textEnemyName.text = defaultTextEnemyName;
            enemyBook.imageEnemyInfo.sprite = defaultImageEnemyInfo;
            enemyBook.textEnemyExplain1.text = defaultTextEnemyExplain1;
            enemyBook.textEnemyExplain2.text = defaultTextEnemyExplain2;
            enemyBook.textEnemyLongDesc.text = textEnemyLongDesc;
        }
        else
        {
            if (enemyList.IndexOf(this.gameObject) != -1)
            {
                index_ = enemyList.IndexOf(this.gameObject);
                enemyBook.textEnemyName.text = EnemyManager.Instance.enemyName[index_];
                enemyBook.imageEnemyInfo.sprite = EnemyManager.Instance.imageEnemyInfo[index_];
                enemyBook.textEnemyExplain1.text = EnemyManager.Instance.textEnemyExplain1[index_];
                enemyBook.textEnemyExplain2.text = EnemyManager.Instance.textEnemyExplain2[index_];
                enemyBook.textEnemyLongDesc.text = EnemyManager.Instance.textEnemyLongDesc[index_];
            }

            if (bossList.IndexOf(this.gameObject) != -1)
            {
                index_ = bossList.IndexOf(this.gameObject);
                enemyBook.textEnemyName.text = EnemyManager.Instance.bossName[index_];
                enemyBook.imageEnemyInfo.sprite = EnemyManager.Instance.imageBossInfo[index_];
                enemyBook.textEnemyExplain1.text = EnemyManager.Instance.textBossExplain1[index_];
                enemyBook.textEnemyExplain2.text = EnemyManager.Instance.textBossExplain2[index_];
                enemyBook.textEnemyLongDesc.text = EnemyManager.Instance.textBossLongDesc[index_];
            }
        }
    }
}
