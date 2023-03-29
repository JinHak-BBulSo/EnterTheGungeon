using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class OptionButtonController : MonoBehaviour
{
    [SerializeField]
    [ListDrawerSettings(Expanded = true, DraggableItems = true, NumberOfItemsPerPage = 5)]
    List<OptionButtonHandler> buttonHandlers = default;


    public static bool detailOptionActive = false;
    public GameObject optionMenu = default;
    public GameObject detailOption = default;

    public GameObject[] detailSroll = new GameObject[4];


    int buttonIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        //buttonHandlers = new List<ButtonHandler>();
        buttonHandlers = transform.GetComponentsInChildren<OptionButtonHandler>().ToList();
        for (int i = 0; i < buttonHandlers.Count; i++)
        {
            buttonHandlers[i].optionButtonController = this;
            buttonHandlers[i].buttonIndex = i;
        }

        buttonIndex = 0;
        ActivateSingleButton(buttonIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //세부옵션이 꺼져있을떄만 동작한다.
        if (detailOptionActive == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + (-1)));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + 1));
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
            {
                DetailOptionSelect();
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
            {
                OptionMenuDisable();
            }
        }
        else if (detailOptionActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X))
            {
                detailOptionDisable();
            }
        }

    }

    private void OnDisable()
    {
        buttonIndex = 0;
        ActivateSingleButton(buttonIndex);
    }
    public void detailOptionDisable()
    {
        StartCoroutine(DetailScaleDisable());
    }


    public void DetailOptionSelect()
    {
        StartCoroutine(DetailScaleActive());
    }

    public void OptionMenuDisable()
    {
        StartCoroutine(OptionScaleDisable());
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
    IEnumerator DetailScaleDisable()
    {
        detailOption.transform.DOScale(0.8f, 0.3f).SetEase(Ease.InBack).SetAutoKill();
        yield return new WaitForSeconds(0.3f);
        detailOption.SetActive(false);
        detailSroll[buttonIndex].SetActive(false);
        detailOptionActive = false;
        Debug.Log($"detailOptionDisable");
    }

    IEnumerator DetailScaleActive()
    {
        detailOptionActive = true;
        detailOption.SetActive(true);
        detailOption.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetAutoKill();
        yield return new WaitForSeconds(0.3f);
        detailSroll[buttonIndex].SetActive(true);
        Debug.Log($"detailSrollActive");
    }

    IEnumerator OptionScaleDisable()
    {
        optionMenu.transform.DOScale(0.8f, 0.3f).SetEase(Ease.InBack).SetAutoKill();
        yield return new WaitForSeconds(0.3f);
        TitleButtonController.optionActive = false;
        optionMenu.SetActive(false);
    }
}
