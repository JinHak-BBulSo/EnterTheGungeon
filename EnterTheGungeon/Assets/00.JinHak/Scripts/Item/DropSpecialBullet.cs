using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpecialBullet : DropPassive
{
    public override void GetPassive()
    {
        SoundManager.Instance.Play("Obj/item_pickup_01", Sound.SFX);
        PlayerManager.Instance.player.playerDamage += 2;
    }
}
