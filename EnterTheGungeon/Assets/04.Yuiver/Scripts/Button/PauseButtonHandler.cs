using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButtonHandler : BaseButtonHandler, IPointerEnterHandler, IPointerClickHandler
{
    public PauseButtonController pauseButtonController = default;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //마우스가 올라갔을때 마우스가 올라간 버튼을 활성화하고 인덱스를 바꾸는 코드
        pauseButtonController.ActivateSingleButton(buttonIndex);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (buttonIndex)
        {
            case 0:
                pauseButtonController.ResumeGame();
                break;
            case 1:
                pauseButtonController.AmmonomiconActive();
                break;
            case 2:
                Debug.Log("옵션창 열기");
                break;
            case 3:
                Debug.Log("빠른재시작인데 여기에 어떻게 활성화 조건을 걸지 모르겠음");
                //GFunc.LoadScene("");
                break;
            case 4:
                Debug.Log("게임 종료 GFunc에서 호출하면 되는데 정말종료할거냐는 메뉴창을 만들어야해서 제외");
                //GFunc.QuitThisGame();
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
