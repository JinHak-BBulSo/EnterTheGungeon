using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : DropItem
{
    public GameObject dropWeapon = default;
    public override void GetDropItem()
    {
        InventoryManager.Instance.inventoryControl.AddItem(this);
    }

    private void Start()
    {
        item.itemScript = dropWeapon.GetComponent<PlayerWeapon>().weaponDataTxt;
    }
}
