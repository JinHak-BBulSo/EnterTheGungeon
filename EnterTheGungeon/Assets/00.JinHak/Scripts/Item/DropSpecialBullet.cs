using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpecialBullet : DropPassive
{
    public override void GetDropItem()
    {
        PlayerManager.Instance.player.playerDamage += 2;
        GetPassive();
    }
}
