using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField]
    List<PauseButtonHandler> buttonHandlers = default;

    public UIController uIController = default;

    private int buttonIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        //buttonHandlers = new List<ButtonHandler>();
        buttonHandlers = transform.GetComponentsInChildren<PauseButtonHandler>().ToList();
        for (int i = 0; i < buttonHandlers.Count; i++)
        {
            buttonHandlers[i].pauseButtonController = this;
            buttonHandlers[i].buttonIndex = i;
        }

        buttonIndex = 0;
        ActivateSingleButton(buttonIndex);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && InventoryControl.isOpenInven == false)
        {
            ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + (-1)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && InventoryControl.isOpenInven == false)
        {
            ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + 1));
        }

        if (Input.GetKeyDown(KeyCode.Z) && InventoryControl.isOpenInven == false ||
            (Input.GetKeyDown(KeyCode.Return)) && InventoryControl.isOpenInven == false)
        {
            switch (buttonIndex)
            {
                case 0:
                    UIController.ResumeGame();
                    break;
                case 1:
                    AmmonomiconActive();
                    break;
                case 2:
                    uIController.OptionActive();
                    break;
                case 3:
                    Debug.Log("빠른재시작인데 여기에 어떻게 활성화 조건을 걸지 모르겠음");
                    //GFunc.LoadScene("");
                    break;
                case 4:
                    GFunc.QuitThisGame();
                    break;
                default:
                    break;
            }
        }
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

    public void AmmonomiconActive()
    {

        //if (InventoryControl.isOpenInven == true)
        //{
        //    Time.timeScale = 1.0f;
        //    InventoryControl.isOpenInven = false;
        //    InventoryManager.Instance.inventoryDataObjs.SetActive(false);
        //    return;
        //}

        InventoryControl.isOpenInven = true;
        InventoryManager.Instance.inventoryDataObjs.SetActive(true);
        // 인벤토리 초기값 설정
        InventoryManager.Instance.inventoryDatas.invenListData.OnFirstViewTab();

    }

}
