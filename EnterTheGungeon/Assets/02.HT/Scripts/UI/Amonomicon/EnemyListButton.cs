using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyListButton : MonoBehaviour
{
    EnemyBook enemyBook;
    List<GameObject> enemyList;

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
        int index_ = enemyList.IndexOf(this.gameObject);

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
            enemyBook.textEnemyName.text = EnemyManager.Instance.enemyName[index_];
            enemyBook.imageEnemyInfo.sprite = EnemyManager.Instance.imageEnemyInfo[index_];
            enemyBook.textEnemyExplain1.text = EnemyManager.Instance.textEnemyExplain1[index_];
            enemyBook.textEnemyExplain2.text = EnemyManager.Instance.textEnemyExplain2[index_];
            enemyBook.textEnemyLongDesc.text = EnemyManager.Instance.textEnemyLongDesc[index_];
        }
    }
}
