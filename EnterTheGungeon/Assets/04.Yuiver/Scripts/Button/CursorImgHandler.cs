using SaveData;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CursorImgHandler : MonoBehaviour
{


    OptionState loadCursorOptionData = default;


    public int seclectMouse = default;
    public Image myCursorImg = default;
    public Texture2D[] CursorTexture = default;
    private void Awake()
    {
        seclectMouse = 0;
        myCursorImg = gameObject.GetComponent<Image>();
        //CursorTexture = Resources.LoadAll<Texture2D>("MOUSE");
    }
    private void OnEnable()
    {
        loadCursorOptionData = DataManager.Instance.LoadOptionGameData();
        seclectMouse = loadCursorOptionData.mouseCursor;
        CursorImgChange(seclectMouse);
        Debug.Log(seclectMouse);
    }
    public void CursorImgChange(int cursorImgNum)
    {
        if (myCursorImg == null)
        { 
            /*Do Nothing*/
        }
        else
        { 
            myCursorImg.sprite = ConvertTexture2DToSprite(DataManager.Instance.cursorImg[cursorImgNum]);
        }
    }
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
    public void ClickDefault()
    {
        loadCursorOptionData = DataManager.Instance.LoadOptionGameData();
        seclectMouse = loadCursorOptionData.mouseCursor;
        CursorImgChange(seclectMouse);
    }

    public Sprite ConvertTexture2DToSprite(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        return Sprite.Create(texture, rect, pivot);
    }

}
