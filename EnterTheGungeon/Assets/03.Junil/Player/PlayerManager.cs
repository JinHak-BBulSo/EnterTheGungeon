using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : GSingleton<PlayerManager>
{
    
    public PlayerController player = default;
    public Room nowPlayerInRoom = default;
    public MoveCamera2D playerCamera = default;
    public MiniCamController miniCamController = default;

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
