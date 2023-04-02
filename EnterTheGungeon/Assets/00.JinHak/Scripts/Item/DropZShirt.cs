using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZShirt : DropItem
{
    public override void GetDropItem()
    {
        SoundManager.Instance.Play("Obj/ammo_pickup_01", Sound.SFX);
        PlayerManager.Instance.player.playerMove.playerSpeed += 1;
    }
}
