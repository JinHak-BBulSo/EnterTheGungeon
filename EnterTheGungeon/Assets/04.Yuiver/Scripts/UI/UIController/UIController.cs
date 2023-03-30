using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static GameObject gamePause = default;
    public static bool boolGamePause = default;

    public GameObject optionMenu = default;

    // Start is called before the first frame update
    void Start()
    {
        gamePause = transform.GetChild(0).gameObject;
        boolGamePause = false;
        gamePause.SetActive(false);
        //optionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            boolGamePause = true;
            gamePause.SetActive(true);
            Time.timeScale = 0.0f;
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
        optionMenu.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetAutoKill();
        OptionButtonController.detailOptionActive = true;
        yield return new WaitForSeconds(0.3f);
    }
}
