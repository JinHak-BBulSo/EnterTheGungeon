using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRoom : Room
{
    ShopKeeperController shopkeeperCtrl = default;

    public override void Start()
    {
        isRoomClear = true;
        shopkeeperCtrl = transform.GetChild(3).GetComponent<ShopKeeperController>();
        shopkeeperCtrl.belongRoom = this;
    }
}
