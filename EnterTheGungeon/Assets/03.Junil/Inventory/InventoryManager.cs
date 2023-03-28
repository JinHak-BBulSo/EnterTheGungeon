using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : GSingleton<InventoryManager>
{
    public InventoryControl inventoryControl = default;
    public InventoryDatas inventoryDatas = default;
    public GameObject inventoryDataObjs = default;

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
