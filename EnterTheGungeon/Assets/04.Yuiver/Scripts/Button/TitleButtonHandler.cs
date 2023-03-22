using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleButtonHandler : BaseButtonHandler, IPointerEnterHandler, IPointerClickHandler
{

    public TitleButtonController titleButtonController = default;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //마우스가 올라갔을때 마우스가 올라간 버튼을 활성화하고 인덱스를 바꾸는 코드
        titleButtonController.ActivateSingleButton(buttonIndex);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭한 버튼에 해당하는 코드를 실행한다.
        switch (buttonIndex)
        {
            case 0:
                //Debug.Log("게임 플레이 씬 이름을 넣어주세요. 아직 설정하지 않았습니다.");
                GFunc.LoadScene("03.LobbyScene");
                break;
            case 1:
                titleButtonController.OptionMenuActive();
                break;
            case 2:
                GFunc.QuitThisGame();
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        // 오브젝트가 활성화 됬을때 버튼에 해당하는 텍스트의 색을 변경하기 위해 버튼을 모아둔 오브젝트의 자식에서 컴포넌트를 가져온다.
        buttonText = this.transform.GetChild(0).GetComponent<TMP_Text>();
    }
}
