using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPassive : DropItem
{
    public override void GetDropItem()
    {
        InventoryManager.Instance.inventoryControl.AddItem(this);
        GetPassive();
    }
    public virtual void GetPassive()
    {
        /* Override Using */
    }
}
