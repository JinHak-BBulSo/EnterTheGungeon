using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerClickHandler
{

    public Item slotItem = default;

    private Image slotImage = default;

    private void Awake()
    {
        slotImage = gameObject.GetComponentMust<Image>();
        
        
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void SetSlotData()
    {
        slotImage.sprite = slotItem.itemSprite;
        slotImage.SetNativeSize();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GFunc.Log("클릭 이벤트 발생");
        InvenInfoData invenInfoData_ = InventoryManager.Instance.inventoryDatas.invenInfoData;


        // 아이템 이름 변경
        invenInfoData_.itemNameTxt.text = slotItem.itemName;

        // 아이템 그림자 켜기
        invenInfoData_.itemTrueImg.SetActive(true);

        // 아이템 이미지로 변경
        Color itemColor_ = invenInfoData_.itemImg.color;
        itemColor_.a = 1;
        invenInfoData_.itemImg.color = itemColor_;
        invenInfoData_.itemImg.sprite = slotItem.itemSprite;

        // 무기의 한줄 설명
        invenInfoData_.itemDescTxt.text = slotItem.itemDescTxt;

        // 무기의 역할
        invenInfoData_.itemTypeTxt.text = slotItem.itemTypeTxt;

        // 아이템 상세 설명
        invenInfoData_.itemTxt.text = slotItem.itemScript;


    }
}
