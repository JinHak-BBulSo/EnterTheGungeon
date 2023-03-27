using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPassive : DropItem
{
    public void GetPassive()
    {
        PlayerInventory.Instance.playerInvenList.passiveItems.Add(this.item);
    }
}
