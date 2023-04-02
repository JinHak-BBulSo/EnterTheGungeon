using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropActive : DropItem
{
    public GameObject activeitem = default;

    public override void GetDropItem()
    {
        SoundManager.Instance.Play("Obj/item_pickup_01", Sound.SFX);
        InventoryManager.Instance.inventoryControl.AddItem(this);
    }
}
