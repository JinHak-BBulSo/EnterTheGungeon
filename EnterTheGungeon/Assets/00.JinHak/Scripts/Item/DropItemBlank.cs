using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemBlank : DropItem
{
    public override void GetDropItem()
    {
        SoundManager.Instance.Play("Obj/item_pickup_01", Sound.SFX);
        PlayerController player = PlayerManager.Instance.player;
        player.playerBlank++;
        player.blankController.SetPlayerBlank(player.playerBlank);
    }
}
