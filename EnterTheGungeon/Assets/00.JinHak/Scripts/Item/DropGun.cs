using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : DropItem
{
    public GameObject dropWeapon = default;
    public override void GetDropItem()
    {
        PlayerInventory.Instance.playerInvenList.gunInvenSlots.Add(this.dropWeapon);
        GetGun();
    }

    public void GetGun()
    {
        PlayerInventory.Instance.playerInvenList.weaponItems.Add(this.item);
    }
}
