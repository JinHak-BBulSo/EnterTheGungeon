using SaveData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorImgHandler : MonoBehaviour
{
    OptionState loadCursorOptionData = default; //현재 저장된 커서 이미지 데이터 파일을 담아두는 변수

    public int seclectMouse = default;  //현재 선택중인 마우스의 인덱스값을 담아두는 변수
    public Image myCursorImg = default; //현재 선택중인 마우스를 출력해줄 이미지를 인스펙터에서 넣기 위한 변수
    public Texture2D[] CursorTexture = default; //텍스쳐를 한번에 불러오기 위한 배열
    private void Awake()
    {
        seclectMouse = 0;
        myCursorImg = gameObject.GetComponent<Image>();
    }
    //메뉴가 활성화 될 경우 선택한 커서 텍스쳐를 스프라이트 변환해서 이미지에 띄워주는 함수
    private void OnEnable()
    {
        loadCursorOptionData = DataManager.Instance.LoadOptionGameData();
        seclectMouse = loadCursorOptionData.mouseCursor;
        CursorImgChange(seclectMouse);
        Debug.Log(seclectMouse);
    }
    // 커서 이미지를 변경해주는 함수
    public void CursorImgChange(int cursorImgNum)
    {
        if (myCursorImg == null) //커서 이미지가 비어있는 경우에 Null Reference를 비하기 위해 추가한 방어 코드
        { 
            /*Do Nothing*/
        }
        else
        { 
            myCursorImg.sprite = ConvertTexture2DToSprite(DataManager.Instance.cursorImg[cursorImgNum]);
        }
    }
    // 오른쪽 버튼을 눌렀다면 커서 이미지를 변경해주는 함수
    public void ClickCursorChangerRight()
    {
        if (seclectMouse == 5)
        {
            seclectMouse = 0;
            CursorImgChange(seclectMouse);
        }
        else
        {
            seclectMouse++;
            CursorImgChange(seclectMouse);
        }
    }
    // 왼쪽 버튼을 눌렀다면 커서 이미지를 변경해주는 함수
    public void ClickCursorChangerLeft()
    {
        if (seclectMouse == 0)
        {
            seclectMouse = 5;
            CursorImgChange(seclectMouse);
        }
        else
        {
            seclectMouse--;
            CursorImgChange(seclectMouse);
        }
    }
    // 세부 옵션안에서 디폴트를 누를 경우에 이미지를 변경해주는 함수
    public void ClickDefault()
    {
        loadCursorOptionData = DataManager.Instance.LoadOptionGameData();
        seclectMouse = loadCursorOptionData.mouseCursor;
        CursorImgChange(seclectMouse);
    }
    //! 텍스쳐를 받아서 스프라이트로 변환해주는 함수
    public Sprite ConvertTexture2DToSprite(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(texture, rect, pivot);
    }

}
