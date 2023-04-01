using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerManager : GSingleton<PlayerManager>
{

    public PlayerController player = default;
    public Room nowPlayerInRoom = default;
    public MoveCamera2D playerCamera = default;
    public MiniCamController miniCamController = default;

    // { [HT] add variables
    public string playerClass;

    // @brief location default = Keep. if kill boss bulletKing, change to Gungeon Proper.
    public string location = "Keep";

    public int totalCoin;
    public int enemyKillCount;

    public float startTime = default;
    public TimeSpan playtime;

    public string lastHitEnemyName;

    // } [HT] add variables

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

    public void EquipWeapon()
    {

    }
    public void ResetWeapon()
    {

    }
}
