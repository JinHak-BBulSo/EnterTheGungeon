using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeathPage : MonoBehaviour
{
    DeadScreen deadScreen;
    TMP_Text causeOfDeathText;


    Transform deathPage2;
    Image deadScreenShot;

    GameObject playerClass;
    TMP_Text playerClassText;

    GameObject deadLocation;
    TMP_Text deadLocationText;

    GameObject playTime;
    TMP_Text playTimeText;


    GameObject totalCoin;
    TMP_Text totalCoinText;

    GameObject enemyKillCount;
    TMP_Text enemyKillCountText;




    // Start is called before the first frame update
    void Start()
    {
        deadScreen = transform.parent.parent.parent.parent.GetChild(4).GetComponent<DeadScreen>();

        deathPage2 = transform.parent.parent.GetChild(2).GetChild(3);
        deadScreenShot = deathPage2.GetChild(0).GetComponent<Image>();
        causeOfDeathText = deathPage2.GetChild(3).GetChild(0).GetComponent<TMP_Text>();

        playerClass = transform.GetChild(5).gameObject;
        playerClassText = playerClass.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

        deadLocation = transform.GetChild(6).gameObject;
        deadLocationText = deadLocation.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

        playTime = transform.GetChild(7).gameObject;
        playTimeText = playTime.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

        totalCoin = transform.GetChild(8).gameObject;
        totalCoinText = totalCoin.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

        enemyKillCount = transform.GetChild(9).gameObject;
        enemyKillCountText = enemyKillCount.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        deadScreenShot.sprite = deadScreen.screenShot;

        playerClassText.text = PlayerManager.Instance.playerClass;
        deadLocationText.text = PlayerManager.Instance.location;
        playTimeText.text = PlayerManager.Instance.playtime.ToString(@"hh\:mm\:ss");
        totalCoinText.text = PlayerManager.Instance.totalCoin.ToString();
        enemyKillCountText.text = PlayerManager.Instance.enemyKillCount.ToString();

        causeOfDeathText.text = PlayerManager.Instance.lastHitEnemyName;
    }
}
