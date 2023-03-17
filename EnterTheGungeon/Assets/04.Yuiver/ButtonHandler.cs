using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler
{
    public static Color OnButtonColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);  ///버튼이 켜질때의 컬러
    public static Color OffButtonColor = new Color(0.5f, 0.5f, 0.5f, 1.0f); ///버튼이 꺼질때의 컬러

    TMP_Text buttonText = null;
    bool isButtonON = false;
    public DoubleInteractionButtonController DIBController = default;
    public int buttonIndex = -1;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ButtonSelect(true);
        DIBController.ActivateSingleButton(buttonIndex);
    }

    void Awake() 
    {
        buttonText = this.transform.GetChild(0).GetComponent<TMP_Text>();
        isButtonON = false;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSelect(bool isOn_)
    {
        if (isOn_)
        {
            buttonText.color = OnButtonColor;
            isButtonON = true;
        }
        else
        { 
            buttonText.color = OffButtonColor;
            isButtonON = false;
        }
    }
}
