using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : GSingleton<InventoryManager>
{
    public static int MAX_SLOT_COUNT = 10;

    public InventoryDatas inventoryDatas = default;
    public GameObject inventoryDataObjs = default;
    public InventoryControl inventoryControl = default;

    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
    }

    protected override void Init()
    {
        
    }

}
