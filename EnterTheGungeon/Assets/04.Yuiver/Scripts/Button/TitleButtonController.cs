using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TitleButtonController : MonoBehaviour
{
    [SerializeField]
    List<TitleButtonHandler> buttonHandlers = default;

    public GameObject optionMenu = default;

    private int buttonIndex = -1;

    public static bool optionActive = false;

    private void Awake()
    {
        optionActive = false;
    }

    private void Start()
    {
        //buttonHandlers = new List<ButtonHandler>();
        buttonHandlers = transform.GetComponentsInChildren<TitleButtonHandler>().ToList();
        for (int i = 0; i < buttonHandlers.Count; i++)
        {
            buttonHandlers[i].titleButtonController = this;
            buttonHandlers[i].buttonIndex = i;
        }

        buttonIndex = 0;
        ActivateSingleButton(buttonIndex);
    }


    private void Update()
    {
        // 옵션메뉴가 켜져있다면 타이틀 메뉴는 키보드로 동작하지 않는다.
        if (optionActive == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + (-1)));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + 1));
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
            {
                buttonIndex = 2;
                ActivateSingleButton(buttonIndex);
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
            {
                switch (buttonIndex)
                {
                    case 0:
                        //Debug.Log("게임 플레이 씬 이름을 넣어주세요. 아직 설정하지 않았습니다.");
                        GFunc.LoadScene("03.LobbyScene");
                        break;
                    case 1:
                        OptionMenuActive();
                        break;
                    case 2:
                        GFunc.QuitThisGame();
                        break;
                    default:
                        break;
                }
            }
        }

    }
    public void OptionMenuActive()
    {
        StartCoroutine(OptionScaleActive());
    }
    IEnumerator OptionScaleActive()
    {
        optionActive = true;
        optionMenu.SetActive(true);
        optionMenu.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetAutoKill();
        yield return new WaitForSeconds(0.3f);
    }


    public void ActivateSingleButton(int index)
    {
        if (buttonHandlers.IsValid() == false)
        {
            buttonIndex = -1;
        }
        else
        {
            for (int i = 0; i < buttonHandlers.Count; i++)
            {
                buttonHandlers[i].ButtonSelect(i == index);

                if (i == index)
                {
                    buttonIndex = index;
                }
            }
        }
    }

    private int LimitKeyBoardIndex(int index_)
    {
        int resultIndex = index_;
        if (index_ >= buttonHandlers.Count)
        {
            resultIndex = 0;
        }
        else if (index_ < 0)
        {
            resultIndex = buttonHandlers.Count - 1;
        }

        return resultIndex;
    }
}
