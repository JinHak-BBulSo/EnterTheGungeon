using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : ActiveItem
{
    public override void UseActive()
    {
        PlayerController player = PlayerManager.Instance.player;
        player.playerHp += 1;
        player.playerMove.playerSpeed += 1.5f;
        player.hpController.SetPlayerHp(player.playerHp, player.playerMaxHp, player.playerShield);
    }
}
