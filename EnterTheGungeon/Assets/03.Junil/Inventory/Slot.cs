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
        invenInfoData_.itemImg.sprite = slotItem.itemSprite;


    }
}
