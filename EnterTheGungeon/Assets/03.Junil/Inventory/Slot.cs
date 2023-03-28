using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{

    public Item slotItem = default;

    private Image slotImage = default;

    private void Awake()
    {
        slotImage = gameObject.GetComponentMust<Image>();
    }

    public void SetSlotData()
    {
        slotImage.sprite = slotItem.itemSprite;
    }
}
