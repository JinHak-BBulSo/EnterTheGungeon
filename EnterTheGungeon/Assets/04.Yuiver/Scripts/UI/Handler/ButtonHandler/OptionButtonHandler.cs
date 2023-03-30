using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OptionButtonHandler : BaseButtonHandler, IPointerEnterHandler, IPointerClickHandler
{
    public OptionButtonController optionButtonController = default;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //마우스가 올라갔을때 마우스가 올라간 버튼을 활성화하고 인덱스를 바꾸는 코드
        optionButtonController.ActivateSingleButton(buttonIndex);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭한 버튼에 해당하는 코드를 실행한다.

        optionButtonController.DetailOptionSelect();
    
    }
    void Awake() 
    {
        // 오브젝트가 활성화 됬을때 버튼에 해당하는 텍스트의 색을 변경하기 위해 버튼을 모아둔 오브젝트의 자식에서 컴포넌트를 가져온다.
        buttonText = this.transform.GetChild(0).GetComponent<TMP_Text>();
    }
}
