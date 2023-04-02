using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemKey : DropItem
{
    public override void GetDropItem()
    {
        SoundManager.Instance.Play("Obj/item_pickup_01", Sound.SFX);
        PlayerController player = PlayerManager.Instance.player;
        player.playerKey++;
        player.keyController.SetPlayerKey(player.playerKey);
    }
}
