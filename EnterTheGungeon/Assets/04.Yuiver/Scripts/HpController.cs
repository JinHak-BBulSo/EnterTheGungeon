using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    public int PlayerHp = default;
    public const int PLAYER_Display_MAX_HP = 44;

    [ShowInInspector]
    List<HpElement> hpObjList = new List<HpElement>();


    void Start()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            hpObjList.Add(transform.GetChild(i).GetComponent<HpElement>());
        }

        SetPlayerHp(5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerHp(int playerHp_)
    {
        PlayerHp = playerHp_;
        int tempPlayerHp = PlayerHp;

        for (int i = 0; i < (int)Mathf.CeilToInt(PlayerHp * 0.5f); i++)
        {
            hpObjList[i].GetComponent<Image>().enabled = true;
            if (tempPlayerHp - 2 >= 0)
            {
                tempPlayerHp -= 2;
                hpObjList[i].HpImgChanger(2);
            }
            else
            { 
                hpObjList[i].HpImgChanger(tempPlayerHp);
            }
        }
        for (int i = 0; i < (int)(transform.childCount - PlayerHp * 0.5f); i++)
        {
            hpObjList[(int)Mathf.CeilToInt(PlayerHp * 0.5f) + i].GetComponent<Image>().enabled = false;
        }
    }
}
