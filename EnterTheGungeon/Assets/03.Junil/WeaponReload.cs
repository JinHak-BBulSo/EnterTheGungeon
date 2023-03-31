using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReload : MonoBehaviour
{

    private float MaxWidth = default;

    public GameObject reloadBarImg = default;

    public bool isReload = false;

    private void Awake()
    {

        reloadBarImg = gameObject.transform.GetChild(0).gameObject;

        // 재장전 바의 최대 길이
        MaxWidth = gameObject.GetComponent<RectTransform>().rect.width;

        gameObject.transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);

        isReload = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    //! 각 총들의 재장전 시간에 맞게 동작하는 함수
    public void ReloadStart(float weaponReload_)
    {
        if (isReload == true) { return; }

        isReload = true;
        PlayerManager.Instance.player.playerAttack.isReloadNow = isReload;


        RectTransform rectTransform = reloadBarImg.transform as RectTransform;

        rectTransform.anchoredPosition = Vector2.left;

        float secondSpeed_ = weaponReload_ * 10;
        float secondWidth_ = MaxWidth / secondSpeed_;

        GFunc.Log($"{secondSpeed_}");

        StartCoroutine(BarMove(secondSpeed_, secondWidth_));
        

    }

    IEnumerator BarMove(float secondSpeed_, float secondWidth_)
    {
        int chkCount_ = 0;

        while (chkCount_ < secondSpeed_)
        {
            reloadBarImg.transform.localPosition += new Vector3(secondWidth_, 0f, 0f);
            chkCount_++;
            yield return new WaitForSeconds(0.1f);
        }

        isReload = false;
        PlayerManager.Instance.player.playerAttack.isReloadNow = isReload;

    }

}
