using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class DoubleInteractionButtonController : MonoBehaviour
{
    List<ButtonHandler> buttonHandlers = default;
    int buttonIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        //buttonHandlers = new List<ButtonHandler>();
        buttonHandlers = transform.GetComponentsInChildren<ButtonHandler>().ToList();
        for (int i = 0; i < buttonHandlers.Count; i++)
        {
            buttonHandlers[i].DIBController = this;
            buttonHandlers[i].buttonIndex = i;
        }

        buttonIndex = 0;
        ActivateSingleButton(buttonIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + (-1)));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ActivateSingleButton(LimitKeyBoardIndex(buttonIndex + 1 ));
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
        Debug.Log($"buttonIndex:{buttonIndex} index_:{index_}, resultIndex: {resultIndex}");

        return resultIndex;
    }
}
