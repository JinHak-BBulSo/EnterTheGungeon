using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpecialBullet : DropPassive
{
    public override void GetPassive()
    {
        PlayerManager.Instance.player.playerDamage += 2;
    }
}
