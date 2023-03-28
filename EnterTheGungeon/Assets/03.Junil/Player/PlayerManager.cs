using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : GSingleton<PlayerManager>
{
    private bool isClicked = false;


    public PlayerController player = default;
    public PlayerWeapon nowEquipWeapon = default;
    

    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        isClicked = false;
    }


    ////! 인벤토리를 여는 함수
    //public void OpenInventory(bool isClick)
    //{
    //    player.isOnInventory = isClick;
    //    playerInventoryObj.SetActive(isClick);
    //}

    protected override void Init()
    {
        GFunc.Log("싱글톤 호출");

    }

    public void EquipWeapon()
    {

    }
    public void ResetWeapon()
    {

    }
}
