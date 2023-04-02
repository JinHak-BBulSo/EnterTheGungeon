using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : DropItem
{
    public GameObject dropWeapon = default;
    public override void GetDropItem()
    {
        SoundManager.Instance.Play("Obj/weapon_pickup_01", Sound.SFX);
        InventoryManager.Instance.inventoryControl.AddItem(this);
    }

    private void Start()
    {
        item.itemScript = dropWeapon.GetComponent<PlayerWeapon>().weaponDataTxt;
        // [Junil] 아이템 정보를 위해 추가
        item.itemTypeTxt = dropWeapon.GetComponent<PlayerWeapon>().weaponType;
        item.itemDescTxt = dropWeapon.GetComponent<PlayerWeapon>().weaponDesc;
    }
}
