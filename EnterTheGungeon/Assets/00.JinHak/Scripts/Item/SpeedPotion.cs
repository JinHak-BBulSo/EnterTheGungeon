using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : ActiveItem
{
    public override void UseActive()
    {
        PlayerManager.Instance.player.playerHp += 1;
        PlayerManager.Instance.player.playerMove.playerSpeed += 1.5f;
    }
}
