using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDatas : MonoBehaviour
{
    public InvenListData invenListData = default;
    public InvenInfoData invenInfoData = default;

    public List<Slot> weaponSlots = default;
    public List<Slot> activeSlots = default;
    public List<Slot> passiveSlots = default;

    public int weaponListCnt = default;
    public int activeListCnt = default;
    public int passiveListCnt = default;


    // 현재 탭을 벗어났는지 확인하는 bool 값
    public bool isOutTabMenu = false;

    public void Awake()
    {
        // 인벤토리에서 수정해야 할 곳들을 캐싱하는 함수
        SetInventory();

        isOutTabMenu = false;
    }



    private void OnDisable()
    {
        // 탭 값을 전부 초기화 하기
        invenListData.AllResetTabInvenMenu();
        invenListData.ResetValTab();
    }

    // Start is called before the first frame update
    public void Start()
    {
        // 플레이어 싱글톤 호출
        InventoryManager.Instance.inventoryDatas = this;
        InventoryManager.Instance.inventoryDataObjs = gameObject;
        GFunc.Log("인벤토리 캐싱 ok");


    }

    //// Update is called once per frame
    //public void Update()
    //{

    //}

    /// @brief 초기 인벤토리를 셋팅하는 함수
    public void SetInventory()
    {
        GameObject ammonomicMenu_ = gameObject.FindChildObj("AmmonomiconMenu");
        invenListData = ammonomicMenu_.FindChildObj("AmmonomiconList").GetComponentMust<InvenListData>();
        invenInfoData = ammonomicMenu_.FindChildObj("AmmonomiconInfo").GetComponentMust<InvenInfoData>();

        weaponListCnt = 0;
        activeListCnt = 0;
        passiveListCnt = 0;

        invenInfoData.SetInvenInfo();
        invenListData.SetInvenList();
    }
}
