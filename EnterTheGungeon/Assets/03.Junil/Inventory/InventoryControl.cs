using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryControl : MonoBehaviour
{
    
    // 인벤토리가 열려있는지의 대한 유무
    public bool isOpenInven = false;


    private void Awake()
    {
        isOpenInven = false;
        transform.parent = InventoryManager.Instance.transform;
    }

    // Start is called before the first frame update
    void Start()
    {

        //AddFirstItem();
        // 인벤토리 매니저 호출
        InventoryManager.Instance.inventoryControl = this;
        InventoryManager.Instance.inventoryDataObjs.SetActive(false);
        GFunc.Log("인벤 컨트롤 호출");
        

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

                
    }

    public void OnViewTabMenuControl()
    {
        // 인벤토리 탭을 어떤 걸 선택했는지 보여줌
        InventoryManager.Instance.inventoryDatas.invenListData.TabInvenMenuVal(
            InventoryManager.Instance.inventoryDatas.invenListData.nowTabInvenCnt);
    }


    public void AddFirstItem()
    {

        int itemListCnt_ = 0;
        Item SPMAWeapon_ = Resources.Load<Item>("03.Junil/Weapon/FirstSetItem/SPMAWeapon");
        GameObject SPMAWeaponPrefab = Resources.Load<GameObject>
            ("03.Junil/Prefabs/PlayerWeapons/01.SPMAWeapon");


        itemListCnt_ = InventoryManager.Instance.inventoryDatas.weaponListCnt;
        Slot weaponSlot = InventoryManager.Instance.inventoryDatas.weaponSlots[itemListCnt_];
        
        weaponSlot.slotItem = SPMAWeapon_;
        weaponSlot.SetSlotData();
        weaponSlot.gameObject.transform.parent.gameObject.SetActive(true);

        // 무기 오브젝트가 들어갈 플레이어의 PlayerAttack 스크립트
        PlayerAttack weaponObjs_ = PlayerManager.Instance.player.playerAttack;

        // playerWeapons 리스트에 무기 추가
        weaponObjs_.playerWeapons.Add(
            Instantiate(SPMAWeaponPrefab,
            weaponObjs_.weaponObjs.transform.position,
            Quaternion.identity,
            weaponObjs_.weaponObjs.transform));

        // playerWeaponScript 리스트에 무기 스크립트 추가
        weaponObjs_.playerWeaponScript.Add(
            weaponObjs_.playerWeapons[itemListCnt_].GetComponentMust<PlayerWeapon>());

        itemListCnt_++;
        InventoryManager.Instance.inventoryDatas.weaponListCnt = itemListCnt_;

    }

    public void AddItem(DropItem dropItem_)
    {
        int itemListCnt_ = 0;

        switch (dropItem_.item.tag)
        {
            case ItemTag.GUN:
                itemListCnt_ = InventoryManager.Instance.inventoryDatas.weaponListCnt;
                Slot weaponSlot = InventoryManager.Instance.inventoryDatas.weaponSlots[itemListCnt_];
                DropGun dropGun_ = dropItem_ as DropGun;

                weaponSlot.slotItem = dropItem_.item;
                weaponSlot.SetSlotData();
                weaponSlot.gameObject.transform.parent.gameObject.SetActive(true);

                // 무기 오브젝트가 들어갈 플레이어의 PlayerAttack 스크립트
                PlayerAttack weaponObjs_ = PlayerManager.Instance.player.playerAttack;

                // playerWeapons 리스트에 무기 추가
                weaponObjs_.playerWeapons.Add(
                    Instantiate(dropGun_.dropWeapon,
                    weaponObjs_.weaponObjs.transform.position,
                    Quaternion.identity,
                    weaponObjs_.weaponObjs.transform));

                // playerWeaponScript 리스트에 무기 스크립트 추가
                weaponObjs_.playerWeaponScript.Add(
                    weaponObjs_.playerWeapons[itemListCnt_].GetComponentMust<PlayerWeapon>());

                weaponObjs_.playerWeapons[itemListCnt_].SetActive(false);


               itemListCnt_++;
                InventoryManager.Instance.inventoryDatas.weaponListCnt = itemListCnt_;
                break;

            case ItemTag.ACTIVE:
                itemListCnt_ = InventoryManager.Instance.inventoryDatas.activeListCnt;
                Slot activeSlot = InventoryManager.Instance.inventoryDatas.activeSlots[itemListCnt_];
                DropActive dropActive_ = dropItem_ as DropActive;

                activeSlot.slotItem = dropItem_.item;
                activeSlot.SetSlotData();
                activeSlot.gameObject.transform.parent.gameObject.SetActive(true);
                PlayerManager.Instance.player.playerActiveItem = dropActive_.activeitem.GetComponent<ActiveItem>();

                itemListCnt_++;
                InventoryManager.Instance.inventoryDatas.activeListCnt = itemListCnt_;
                break;

            case ItemTag.PASSIVE:
                itemListCnt_ = InventoryManager.Instance.inventoryDatas.passiveListCnt;
                Slot passiveSlot = InventoryManager.Instance.inventoryDatas.passiveSlots[itemListCnt_];

                passiveSlot.slotItem = dropItem_.item;
                passiveSlot.SetSlotData();
                passiveSlot.gameObject.transform.parent.gameObject.SetActive(true);

                itemListCnt_++;
                InventoryManager.Instance.inventoryDatas.passiveListCnt = itemListCnt_;
                break;
        }


    }   // AddItem()


}
