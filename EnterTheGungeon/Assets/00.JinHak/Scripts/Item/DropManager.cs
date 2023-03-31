using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropManager : GSingleton<DropManager>
{
    private GameObject treasureBox = default;
    public List<GameObject> dropItems = default;

    public override void Awake()
    {
        base.Awake();
        treasureBox = Resources.Load<GameObject>("00.JinHak/Prefabs/ObjS/TreasureBox");
        dropItems = Resources.LoadAll<GameObject>("00.JinHak/Prefabs/DropItem").ToList();
    }
    public void ItemDrop()
    {
        if (dropItems.Count > 0)
        {
            GameObject boxTransform_ = PlayerManager.Instance.nowPlayerInRoom.GetComponent<Room>().spawnPoints[0];
            GameObject treasureBox_ = Instantiate(treasureBox, boxTransform_.transform.position, Quaternion.identity);
            treasureBox_.transform.parent = PlayerManager.Instance.nowPlayerInRoom.transform;
        }
    }
}
