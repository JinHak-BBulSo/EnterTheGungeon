using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSuit : DropItem
{
    public override void GetDropItem()
    {
        PlayerManager.Instance.player.playerShield += 2;
    }
}
