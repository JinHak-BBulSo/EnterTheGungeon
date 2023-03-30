using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropActive : DropItem
{
    public GameObject activeitem = default;

    public override void GetDropItem()
    {
        InventoryManager.Instance.inventoryControl.AddItem(this);
    }
}
