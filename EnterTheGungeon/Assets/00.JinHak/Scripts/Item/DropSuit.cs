using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSuit : DropItem
{
    public override void GetDropItem()
    {
        SoundManager.Instance.Play("Obj/ammo_pickup_01", Sound.SFX);
        PlayerController player = PlayerManager.Instance.player;
        player.playerShield += 2;
        player.hpController.SetPlayerHp(player.playerHp, player.playerMaxHp, player.playerShield);
    }
}
