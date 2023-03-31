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



    public void Awake()
    {

        // 인벤토리 싱글톤 호출
        InventoryManager.Instance.inventoryDatas = this;
        InventoryManager.Instance.inventoryDataObjs = gameObject;
        
        // 인벤토리에서 수정해야 할 곳들을 캐싱하는 함수
        SetInventory();
        GFunc.Log("인벤 데이터 호출");
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
        //// 인벤토리 싱글톤 호출
        //InventoryManager.Instance.inventoryDatas = this;
        //InventoryManager.Instance.inventoryDataObjs = gameObject;
        //GFunc.Log("인벤 데이터 호출");


    }


    //! 초기 인벤토리를 셋팅하는 함수
    public void SetInventory()
    {
        GameObject ammonomicMenu_ = gameObject.FindChildObj("AmmonomiconMenu");
        invenListData = ammonomicMenu_.FindChildObj("AmmonomiconList").GetComponentMust<InvenListData>();
        invenInfoData = ammonomicMenu_.FindChildObj("AmmonomiconInfo").GetComponentMust<InvenInfoData>();

        weaponListCnt = 0;
        activeListCnt = 0;
        passiveListCnt = 0;

        invenListData.SetInvenList();
        invenInfoData.SetInvenInfo();
    }
}
