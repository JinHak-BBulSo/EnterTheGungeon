using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZShirt : DropItem
{
    public override void GetDropItem()
    {
        PlayerManager.Instance.player.playerMove.playerSpeed += 1;
    }
}
