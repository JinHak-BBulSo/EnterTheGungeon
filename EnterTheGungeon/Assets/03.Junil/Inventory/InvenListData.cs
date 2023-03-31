using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InvenListData : MonoBehaviour
{

    private const int IEVEN_TAB_ICON_CNT = 4;

    // 인벤토리 탭 종류 | 장비, 총, 아이템, 적, 보스
    private GameObject[] equipmentMenu = default;
    private GameObject[] gunMenu = default;
    private GameObject[] itemMenu = default;
    private GameObject[] monsterMenu = default;
    private GameObject[] bossMenu = default;



    // 총, 액티브, 패시브 슬롯 정보를 관리하는 리스트
    public GameObject slotPrefabs = default;


    // 총 아이템을 모아두는 인벤토리
    public GameObject gunsInven = default;

    // 액티브 아이템을 모아두는 인벤토리
    public GameObject activeInven = default;

    // 패시브 아이템을 모아두는 인벤토리
    public GameObject passiveInven = default;


    // { [Junil] 인벤토리 크기를 조절할 이미지들
    public Image gunInvenImage = default;

    public Image activeInvenImage = default;

    public Image passiveInvenImage = default;

    // } [Junil] 인벤토리 크기를 조절할 이미지들


    // { [Junil] 인벤토리를 관리할 리스트와 딕셔너리

    public int invenTabMenu = default;

    private const int IEVEN_TAB_VAL = 5;

    public Dictionary<string, List<Item>> invenDict = default;
    // } [Junil] 인벤토리를 관리할 리스트와 딕셔너리



    // 현재 몇 번째 인벤인지 알려주는 int 값
    public int nowTabInvenCnt = default;

    // 현재 선택된 탭이 무엇인지 알려주는 int 값
    public int selectTabInvenVal = default;



    // Start is called before the first frame update
    void Start()
    {

    }



    //! 켜져있는 슬롯이 5개씩 넘어가면 실행되는 함수
    public void AddHeightImage(List<GameObject> ListName_)
    {
        int tempInt_ = 0;

        for (int i = 0; i < ListName_.Count / 5; i++)
        {
            if (ListName_[i * 5].activeSelf == true)
            {
                if (i == 0) { /* Do Nothing */ }
                else
                {
                    tempInt_++;
                }
            }
        }

        switch (ListName_[0].transform.parent.gameObject.name)
        {
            case "GunsInventory":
                gunInvenImage.rectTransform.sizeDelta = new Vector2(
                    gunInvenImage.rectTransform.sizeDelta.x,
                    gunInvenImage.rectTransform.sizeDelta.y + 24 * tempInt_);
                break;

            case "ActiveInventory":
                activeInvenImage.rectTransform.sizeDelta = new Vector2(
                    activeInvenImage.rectTransform.sizeDelta.x,
                    activeInvenImage.rectTransform.sizeDelta.y + 24 * tempInt_);
                break;

            case "PassiveInventory":
                passiveInvenImage.rectTransform.sizeDelta = new Vector2(
                    passiveInvenImage.rectTransform.sizeDelta.x,
                    passiveInvenImage.rectTransform.sizeDelta.y + 24 * tempInt_);
                break;
        }


    }


    //! 인벤토리 탭을 어떤 걸 선택했는지 보여주는 함수
    public void TabInvenMenuVal(int nowTabInven)
    {
        AllResetTabInvenMenu();

        equipmentMenu[3].SetActive(true);
        gunMenu[3].SetActive(true);
        itemMenu[3].SetActive(true);
        monsterMenu[3].SetActive(true);
        bossMenu[3].SetActive(true);

        if (selectTabInvenVal == nowTabInven)
        {

            switch (nowTabInven)
            {
                case 0:
                    equipmentMenu[3].SetActive(false);
                    equipmentMenu[0].SetActive(true);

                    break;

                case 1:
                    gunMenu[3].SetActive(false);
                    gunMenu[0].SetActive(true);

                    break;

                case 2:
                    itemMenu[3].SetActive(false);
                    itemMenu[0].SetActive(true);

                    break;

                case 3:
                    monsterMenu[3].SetActive(false);
                    monsterMenu[0].SetActive(true);

                    break;

                case 4:
                    bossMenu[3].SetActive(false);
                    bossMenu[0].SetActive(true);

                    break;

            }
        }   // if : 현재 선택된 값과 있는 위치 값이 같은 조건
        else
        {
            switch (nowTabInven)
            {
                case 0:
                    equipmentMenu[3].SetActive(false);

                    equipmentMenu[2].SetActive(true);

                    break;

                case 1:
                    gunMenu[3].SetActive(false);

                    gunMenu[2].SetActive(true);

                    break;

                case 2:
                    itemMenu[3].SetActive(false);

                    itemMenu[2].SetActive(true);

                    break;

                case 3:
                    monsterMenu[3].SetActive(false);

                    monsterMenu[2].SetActive(true);

                    break;

                case 4:
                    bossMenu[3].SetActive(false);

                    bossMenu[2].SetActive(true);

                    break;
            }

            switch (selectTabInvenVal)
            {
                case 0:
                    equipmentMenu[3].SetActive(false);

                    equipmentMenu[1].SetActive(true);

                    break;

                case 1:
                    gunMenu[3].SetActive(false);

                    gunMenu[1].SetActive(true);

                    break;

                case 2:
                    itemMenu[3].SetActive(false);

                    itemMenu[1].SetActive(true);

                    break;

                case 3:
                    monsterMenu[3].SetActive(false);

                    monsterMenu[1].SetActive(true);

                    break;

                case 4:
                    bossMenu[3].SetActive(false);

                    bossMenu[1].SetActive(true);

                    break;
            }


        }
    }



    //! 탭 값을 초기화 해주는 함수
    public void ResetValTab()
    {
        nowTabInvenCnt = 0;
        selectTabInvenVal = nowTabInvenCnt;
    }

    public void OnEnterKeyDown()
    {
        selectTabInvenVal = nowTabInvenCnt;
    }

    //! 현재 인벤토리 탭의 선택 값을 초기화 하는 함수
    public void ResetTabInvenMenu(GameObject[] tabMenu)
    {
        for (int i = 0; i < IEVEN_TAB_ICON_CNT; i++)
        {
            tabMenu[i].SetActive(false);
        }
    }

    //! 모든 인벤토리 탭의 선택 값을 초기화 하는 함수
    public void AllResetTabInvenMenu()
    {
        ResetTabInvenMenu(equipmentMenu);
        ResetTabInvenMenu(gunMenu);
        ResetTabInvenMenu(itemMenu);
        ResetTabInvenMenu(monsterMenu);
        ResetTabInvenMenu(bossMenu);
    }

    //! 인벤토리가 켜질 때마다 먼저 장비가 보이기 하는 함수
    public void OnFirstViewTab()
    {
        // 초기 상태 보여주기
        equipmentMenu[0].SetActive(true);
        gunMenu[3].SetActive(true);
        itemMenu[3].SetActive(true);
        monsterMenu[3].SetActive(true);
        bossMenu[3].SetActive(true);
    }



    public void SetInvenList()
    {
        InventoryDatas inventoryData_ =
            gameObject.transform.parent.
            gameObject.transform.parent.gameObject.GetComponentMust<InventoryDatas>();

        // Slot 프리팹 가져오기
        slotPrefabs = Resources.Load<GameObject>("03.Junil/Prefabs/Inventory/Slot");


        equipmentMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        gunMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        itemMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        monsterMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        bossMenu = new GameObject[IEVEN_TAB_ICON_CNT];


        invenTabMenu = IEVEN_TAB_VAL;
        nowTabInvenCnt = 0;
        selectTabInvenVal = nowTabInvenCnt;



        // 인벤토리 탭을 보여주는 장소
        GameObject invenMenu_ = gameObject.transform.GetChild(0).gameObject;

        List<GameObject> invenTab_ = new List<GameObject>();

        for (int i = 0; i < invenMenu_.transform.childCount; i++)
        {
            invenTab_.Add(invenMenu_.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < IEVEN_TAB_ICON_CNT; i++)
        {
            equipmentMenu[i] = invenTab_[0].transform.GetChild(i).gameObject;
            gunMenu[i] = invenTab_[1].transform.GetChild(i).gameObject;
            itemMenu[i] = invenTab_[2].transform.GetChild(i).gameObject;
            monsterMenu[i] = invenTab_[3].transform.GetChild(i).gameObject;
            bossMenu[i] = invenTab_[4].transform.GetChild(i).gameObject;

            equipmentMenu[i].SetActive(false);
            gunMenu[i].SetActive(false);
            itemMenu[i].SetActive(false);
            monsterMenu[i].SetActive(false);
            bossMenu[i].SetActive(false);
        }


        // 총, 액티브, 패시브 위치를 가져오기
        GameObject inventorySetObjs_ = gameObject.transform.GetChild(2).gameObject;

        for (int i = 0 + 1; i < inventorySetObjs_.transform.childCount; i += 2)
        {
            GameObject nowEquipment_ = inventorySetObjs_.transform.GetChild(i).gameObject;

            GameObject nowEquipmentInven_ = nowEquipment_.transform.GetChild(0).gameObject;

            switch (i)
            {
                case 1:
                    gunsInven = nowEquipmentInven_;
                    gunInvenImage = nowEquipment_.GetComponentMust<Image>();
                    break;

                case 3:
                    activeInven = nowEquipmentInven_;
                    activeInvenImage = nowEquipment_.GetComponentMust<Image>();

                    break;

                case 5:
                    passiveInven = nowEquipmentInven_;
                    passiveInvenImage = nowEquipment_.GetComponentMust<Image>();

                    break;
            }
        }


        // 슬롯은 각각 10개만 넣는다
        // 각각의 리스트에 슬롯 프리팹을 추가한다.
        for (int i = 0; i < InventoryManager.MAX_SLOT_COUNT; i++)
        {
            inventoryData_.weaponSlots.Add(gunsInven.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Slot>());
            inventoryData_.activeSlots.Add(activeInven.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Slot>());
            inventoryData_.passiveSlots.Add(passiveInven.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Slot>());
        }
    }   // SetInvenList()
}
