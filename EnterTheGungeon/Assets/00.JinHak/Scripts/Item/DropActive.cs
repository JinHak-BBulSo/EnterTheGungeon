using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropActive : DropItem
{
    public GameObject activeitem = default;
    public void GetActive()
    {
        PlayerInventory.Instance.playerInvenList.activeItems.Add(this.item);
    }
}
