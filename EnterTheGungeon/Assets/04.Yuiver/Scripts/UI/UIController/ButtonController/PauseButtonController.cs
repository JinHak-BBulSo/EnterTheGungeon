using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    [SerializeField]
    List<PauseButtonHandler> buttonHandlers = default;

    public GameObject gamepause = default;
    public GameObject ammonomicon = default;

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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + (-1)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + 1));
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
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        gamepause.SetActive(false);
    }

    public void AmmonomiconActive()
    {
        ammonomicon.SetActive(true);
    }
}
