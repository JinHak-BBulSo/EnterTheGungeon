using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemHP : DropItem
{
    public override void GetDropItem()
    {
        SoundManager.Instance.Play("Obj/item_pickup_01", Sound.SFX);
        PlayerController player = PlayerManager.Instance.player;
        if (PlayerManager.Instance.player.playerMaxHp < PlayerManager.Instance.player.playerHp)
        {
            player.playerHp++;
            player.hpController.SetPlayerHp(player.playerHp, player.playerMaxHp, player.playerShield);
        }
    }
}
