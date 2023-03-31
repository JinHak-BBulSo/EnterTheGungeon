using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static GameObject gamePause = default;
    public static bool boolGamePause = default;
    public static GameObject optionMenu = default;

    public GameObject Inventory = default;

    // Start is called before the first frame update
    void Start()
    {
        gamePause = transform.GetChild(1).gameObject;
        Inventory = transform.GetChild(2).gameObject;
        optionMenu = transform.GetChild(3).gameObject;
        boolGamePause = false;
        gamePause.SetActive(false);
        optionMenu.SetActive(false);
        //optionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && boolGamePause == false)
        {
            boolGamePause = true;
            gamePause.SetActive(true);
            Time.timeScale = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Inventory.activeSelf == true && boolGamePause == true ||
            Input.GetKeyDown(KeyCode.X) && Inventory.activeSelf == true && boolGamePause == true)
        {
            InventoryControl.isOpenInven = false;
            Inventory.SetActive(false);
        }
    }

    public static void ResumeGame()
    {
        boolGamePause = false;
        Time.timeScale = 1.0f;
        gamePause.SetActive(false);
    }
    public void OptionActive()
    {
        StartCoroutine(UIOptionScaleActive());
    }

    IEnumerator UIOptionScaleActive()
    {
        optionMenu.SetActive(true);
        Tween OptionTween = optionMenu.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetUpdate(UpdateType.Normal, true).SetAutoKill();
        //OptionButtonController.detailOptionActive = true;
        yield return OptionTween.WaitForCompletion();
    }
}
