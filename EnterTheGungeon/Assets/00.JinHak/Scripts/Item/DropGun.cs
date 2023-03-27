using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : DropItem
{
    public GameObject dropWeapon = default;
    public override void GetDropItem()
    {
        //PlayerInvenList에서 웨폰 데이터는 리스트일 필요가 있음
        //PlayerInvenList.weaponObjs.Add(this.dropWeaponData);
        GetGun();
    }

    public void GetGun()
    {
        //PlayerInvenList.weaponItems.Add(this.item);
    }
}
