using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : GSingleton<PlayerManager>
{
    public PlayerController player = default;

    public PlayerInventory playerInventory = default;
    public GameObject playerInventoryObj = default;

    private bool isClicked = false;

    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        isClicked = false;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if(isClicked == true)
            {

                isClicked = false;
                Time.timeScale = 1f;

                OpenInventory(isClicked);
                return;
            }

            isClicked = true;
            Time.timeScale = 0f;
            OpenInventory(isClicked);

        }
    }

    //! 인벤토리를 여는 함수
    public void OpenInventory(bool isClick)
    {
        player.isOnInventory = isClick;
        playerInventoryObj.SetActive(isClick);
    }

    protected override void Init()
    {
        GFunc.Log("싱글톤 호출");

    }
}
