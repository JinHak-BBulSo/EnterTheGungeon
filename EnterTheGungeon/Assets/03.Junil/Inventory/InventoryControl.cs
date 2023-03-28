using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryControl : MonoBehaviour
{

    

    // { [Junil] 무기 프리팹을 가져와서 넣는 배열
    // 무기 프리팹 저장하는 배열
    public GameObject[] playerWeaponPrefabs = default;
    // 무기 이미지를 저장하는 배열
    public Sprite[] playerWeaponSpriteObjs = default;
    // } [Junil] 무기 프리팹을 가져와서 넣는 배열

    // 인벤토리가 열려있는지의 대한 유무
    public bool isOpenInven = false;

    private void Awake()
    {
        isOpenInven = false;

        // 플레이어 무기 프리팹과 그 무기의 이미지를 배열에 저장한다.
        //playerWeaponPrefabs = Resources.LoadAll<GameObject>("03.Junil/Prefabs/PlayerWeapons");
        //playerWeaponSpriteObjs = Resources.LoadAll<Sprite>("03.Junil/Weapon/ItemUseWeapons");
    }

    // Start is called before the first frame update
    void Start()
    {

        //AddFirstWeapon(0);
        // 인벤토리를 꺼버린다.
        InventoryManager.Instance.inventoryDataObjs.SetActive(false);

        // 인벤토리 매니저 호출
        InventoryManager.Instance.inventoryControl = this;

    }

    // Update is called once per frame
    void Update()
    {
        
        // 인벤토리 오픈
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(isOpenInven == true) 
            {
                isOpenInven = false;
                InventoryManager.Instance.inventoryDataObjs.SetActive(false);

                return; 
            }


            isOpenInven = true;
            GFunc.Log("눌림");
            InventoryManager.Instance.inventoryDataObjs.SetActive(true);
            // 인벤토리 초기값 설정
            InventoryManager.Instance.inventoryDatas.invenListData.OnFirstViewTab();

        }


        // 엔터 키 입력 시 발동하는 조건
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GFunc.Log("엔터값 입력");
            // 현재 선택한 탭 값으로 변경
            InventoryManager.Instance.inventoryDatas.invenListData.OnEnterKeyDown();

            // 인벤토리 탭을 어떤 걸 선택했는지 보여줌
            OnViewTabMenuControl();

        }

        // 위 화살표 입력 시
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 0~4 까지의 범위를 범어나지 않게 하기 위한 조건
            if (InventoryManager.Instance.inventoryDatas.invenListData.nowTabInvenCnt == 0) { return; }
            
            InventoryManager.Instance.inventoryDatas.invenListData.nowTabInvenCnt--;

            OnViewTabMenuControl();

        }

        // 아래 화살표 입력 시
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 0~4 까지의 범위를 범어나지 않게 하기 위한 조건
            if (InventoryManager.Instance.inventoryDatas.invenListData.nowTabInvenCnt == 4) { return; }

            InventoryManager.Instance.inventoryDatas.invenListData.nowTabInvenCnt++;

            OnViewTabMenuControl();


        }


        // 탭에서 상세 창으로 넘어가는 조건
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    isOutTabMenu = true;
        //}

        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    isOutTabMenu = false;
        //}
    }

    public void OnViewTabMenuControl()
    {
        // 인벤토리 탭을 어떤 걸 선택했는지 보여줌
        InventoryManager.Instance.inventoryDatas.invenListData.TabInvenMenuVal(
            InventoryManager.Instance.inventoryDatas.invenListData.nowTabInvenCnt);
    }

    public void AddItem(Item dropItem_)
    {

        switch (dropItem_.tag)
        {
            case ItemTag.GUN:


                int tempWeaponVal_ = InventoryManager.Instance.inventoryDatas.weaponListCnt;

                InventoryManager.Instance.inventoryDatas.weaponSlots[tempWeaponVal_].slotItem = dropItem_;
                InventoryManager.Instance.inventoryDatas.weaponSlots[tempWeaponVal_].SetSlotData();

                InventoryManager.Instance.inventoryDatas.gunInvenSlots[tempWeaponVal_].SetActive(true);

                tempWeaponVal_++;

                InventoryManager.Instance.inventoryDatas.weaponListCnt = tempWeaponVal_;

                break;

            case ItemTag.ACTIVE:

                int tempActiveVal_ = InventoryManager.Instance.inventoryDatas.activeListCnt;

                InventoryManager.Instance.inventoryDatas.activeSlots[tempActiveVal_].slotItem = dropItem_;
                InventoryManager.Instance.inventoryDatas.activeSlots[tempActiveVal_].SetSlotData();


                InventoryManager.Instance.inventoryDatas.activeInvenSlots[tempActiveVal_].SetActive(true);

                tempActiveVal_++;

                InventoryManager.Instance.inventoryDatas.activeListCnt = tempActiveVal_;

                break;

            case ItemTag.PASSIVE:

                int tempPassiveVal_ = InventoryManager.Instance.inventoryDatas.passiveListCnt;

                InventoryManager.Instance.inventoryDatas.passiveSlots[tempPassiveVal_].slotItem = dropItem_;
                InventoryManager.Instance.inventoryDatas.passiveSlots[tempPassiveVal_].SetSlotData();

                InventoryManager.Instance.inventoryDatas.passiveInvenSlots[tempPassiveVal_].SetActive(true);

                tempPassiveVal_++;

                InventoryManager.Instance.inventoryDatas.passiveListCnt = tempPassiveVal_;

                break;
        }


    }   // AddItem()
}
