using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHpBar : MonoBehaviour
{

    public string bossName;
    public GameObject boss;

    TMP_Text hpbarBossName;
    Image innerHpbar;
    float hpbarGauge;
    // Start is called before the first frame update
    void Start()
    {
        hpbarBossName = transform.GetChild(0).GetComponent<TMP_Text>();
        innerHpbar = transform.GetChild(1).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null && bossName == "The Gorgun")
        {
            innerHpbar.fillAmount = (float)boss.GetComponent<BossGorGun>().currentHp / (float)boss.GetComponent<BossGorGun>().maxHp;
        }

        if (boss == null)
        {
            Destroy(this.gameObject);
        }
    }
}
