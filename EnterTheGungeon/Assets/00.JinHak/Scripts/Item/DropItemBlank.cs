using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemBlank : DropItem
{
    public override void GetDropItem()
    {
        PlayerManager.Instance.player.playerBlank++;
    }
}
