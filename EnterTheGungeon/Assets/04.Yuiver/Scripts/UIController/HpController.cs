using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public int PlayerHp = default;
    public int playerMaxHp = default;
    public int PlayerShield = default;
    public const int PLAYER_Display_MAX_HP = 44;
    public const int PLAYER_Display_MAX_Shield = 10;

    [ShowInInspector]
    List<HpElement> hpObjList = new List<HpElement>();




    void Start()
    {
        PlayerShield = 2;
        playerMaxHp = 6;

        for (int i = 0; i < transform.childCount; i++)
        {
            hpObjList.Add(transform.GetChild(i).GetComponent<HpElement>());
        }

        SetPlayerHp(4);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerHp(int playerHp_)
    {
        PlayerHp = playerHp_;
        int tempPlayerHp = PlayerHp;
        int ShieldIndex = -1;

        for (int i = 0; i < (int)Mathf.CeilToInt(playerMaxHp * 0.5f); i++)
        {
            hpObjList[i].GetComponent<Image>().enabled = true;
            if (tempPlayerHp - 2 >= 0)
            {
                tempPlayerHp -= 2;
                hpObjList[i].HpImgChanger(2);
            }
            else
            {
                ShieldIndex = i + 1;
                hpObjList[i].HpImgChanger(tempPlayerHp);
            }
        }
        for (int i = 0; i < (int)(transform.childCount - (playerMaxHp * 0.5f + PlayerShield)); i++)
        {
            hpObjList[(int)Mathf.CeilToInt(playerMaxHp * 0.5f + PlayerShield) + i].GetComponent<Image>().enabled = false;
        }
        for (int i = 0; i < PlayerShield; i++)
        {
            hpObjList[ShieldIndex + i].HpImgChanger(3);
        }
    }
}
