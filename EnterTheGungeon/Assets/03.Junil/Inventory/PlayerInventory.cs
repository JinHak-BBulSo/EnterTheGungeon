using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventory : GSingleton<PlayerInventory>
{
    private PlayerInvenList playerInvenList = default;
    private PlayerInvenInfo playerInvenInfo = default;

    // 현재 탭을 벗어났는지 확인하는 bool 값
    public bool isOutTabMenu = false;

    private void Awake()
    {
        // 인벤토리에서 수정해야 할 곳들을 캐싱하는 함수
        SetInventory();

        isOutTabMenu = false;  
    }


    private void OnEnable()
    {
        // 도감이 켜질 때마다 장비 먼저 보이게 하기
        playerInvenList.OnFirstViewTab();
    }

    private void OnDisable()
    {
        // 탭 값을 전부 초기화 하기
        playerInvenList.AllResetTabInvenMenu();
        playerInvenList.ResetValTab();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 싱글톤 호출
        PlayerManager.Instance.playerInventory = this;
        PlayerManager.Instance.playerInventoryObj = gameObject;
        GFunc.Log("인벤토리 캐싱 ok");


        

        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        

        // 엔터 키 입력 시 발동하는 조건
        if (Input.GetKeyDown(KeyCode.Return))
        {
            playerInvenList.OnEnterKeyDown();
            GFunc.Log("엔터값 입력");
            playerInvenList.TabInvenMenuVal(playerInvenList.nowTabInvenCnt);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 0~4 까지의 범위를 범어나지 않게 하기 위한 조건
            if (playerInvenList.nowTabInvenCnt == 0) { return; }
            GFunc.Log("인벤 작동");

            playerInvenList.nowTabInvenCnt--;
            playerInvenList.TabInvenMenuVal(playerInvenList.nowTabInvenCnt);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 0~4 까지의 범위를 범어나지 않게 하기 위한 조건
            if (playerInvenList.nowTabInvenCnt == 4) { return; }
            GFunc.Log("인벤 작동");

            playerInvenList.nowTabInvenCnt++;
            playerInvenList.TabInvenMenuVal(playerInvenList.nowTabInvenCnt);

        }


        // 탭에서 상세 창으로 넘어가는 조건
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isOutTabMenu = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isOutTabMenu = false;
        }


    }

        


    public void AddItem(Item item)
    {

        ItemTag itemTag = item.tag;


        switch (itemTag)
        {
            case ItemTag.GUN:
                break;

            case ItemTag.ACTIVE:
                break;

            case ItemTag.PASSIVE:
                break;

            case ItemTag.ETC:
                break;
        }
    }


    
    
    /// @brief 초기 인벤토리를 셋팅하는 함수
    public void SetInventory()
    {
        GameObject ammonomicMenu_ = gameObject.FindChildObj("AmmonomiconMenu");
        playerInvenList = ammonomicMenu_.FindChildObj("AmmonomiconList").GetComponentMust<PlayerInvenList>();
        playerInvenInfo = ammonomicMenu_.FindChildObj("AmmonomiconInfo").GetComponentMust<PlayerInvenInfo>();



        playerInvenList.SetInvenList();
        playerInvenInfo.SetInvenInfo();


    }
}
