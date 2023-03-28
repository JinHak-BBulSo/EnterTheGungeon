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
                weaponSlot.gameObject.SetActive(true);
                Instantiate(dropGun_.dropWeapon, PlayerManager.Instance.player.playerAttack.weaponObjs.transform.position,
                    Quaternion.identity,
                    PlayerManager.Instance.player.playerAttack.weaponObjs.transform);

                itemListCnt_++;
                InventoryManager.Instance.inventoryDatas.weaponListCnt = itemListCnt_;
                break;

            case ItemTag.ACTIVE:
                itemListCnt_ = InventoryManager.Instance.inventoryDatas.activeListCnt;
                Slot activeSlot = InventoryManager.Instance.inventoryDatas.activeSlots[itemListCnt_];
                DropActive dropActive_ = dropItem_ as DropActive;

                activeSlot.slotItem = dropItem_.item;
                activeSlot.SetSlotData();
                activeSlot.gameObject.SetActive(true);
                PlayerManager.Instance.player.playerActiveItem = dropActive_.activeitem.GetComponent<ActiveItem>();

                itemListCnt_++;
                InventoryManager.Instance.inventoryDatas.activeListCnt = itemListCnt_;
                break;

            case ItemTag.PASSIVE:
                itemListCnt_ = InventoryManager.Instance.inventoryDatas.passiveListCnt;
                Slot passiveSlot = InventoryManager.Instance.inventoryDatas.passiveSlots[itemListCnt_];

                passiveSlot.slotItem = dropItem_.item;
                passiveSlot.SetSlotData();
                passiveSlot.gameObject.SetActive(true);

                itemListCnt_++;
                InventoryManager.Instance.inventoryDatas.passiveListCnt = itemListCnt_;
                break;
        }


    }   // AddItem()
}
