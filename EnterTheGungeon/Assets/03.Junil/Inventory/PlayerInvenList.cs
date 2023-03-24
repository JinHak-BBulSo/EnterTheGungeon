using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvenList : MonoBehaviour
{

    private const int IEVEN_TAB_ICON_CNT = 4;

    // 인벤토리 탭 종류 | 장비, 총, 아이템, 적, 보스
    private GameObject[] equipmentMenu = default;
    private GameObject[] gunMenu = default;
    private GameObject[] itemMenu = default;
    private GameObject[] monsterMenu = default;
    private GameObject[] bossMenu = default;


    // { [Junil] 인벤토리를 관리할 리스트와 딕셔너리

    private int invenTabMenu = default;

    private const int IEVEN_TAB_VAL = 5;

    private Dictionary<string, List<Item>> invenDict = default;
    private List<Item> weaponItems = default;
    private List<Item> activeItems = default;
    private List<Item> passiveItems = default;
    // } [Junil] 인벤토리를 관리할 리스트와 딕셔너리


    // 장비 - 총에 들어갈 오브젝트 배열
    public GameObject[] weaponObjs = default;


    // 총 아이템을 모아두는 인벤토리
    public GameObject gunsInven = default;

    // 액티브 아이템을 모아두는 인벤토리
    public GameObject activeInven = default;

    // 패시브 아이템을 모아두는 인벤토리
    public GameObject passiveInven = default;


    // 현재 몇 번째 인벤인지 알려주는 int 값
    public int nowTabInvenCnt = default;

    // 현재 선택된 탭이 무엇인지 알려주는 int 값
    public int selectTabInvenVal = default;


    private void Awake()
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        equipmentMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        gunMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        itemMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        monsterMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        bossMenu = new GameObject[IEVEN_TAB_ICON_CNT];


        invenTabMenu = IEVEN_TAB_VAL;
        nowTabInvenCnt = 0;
        selectTabInvenVal = nowTabInvenCnt;

        // 현재 탭 int 값을 보고 거기에 맞는 리스트를 가져온다
        invenDict = new Dictionary<string, List<Item>>();
        weaponItems = new List<Item>();
        activeItems = new List<Item>();
        passiveItems = new List<Item>();

        invenDict.Add("총", weaponItems);
        invenDict.Add("액티브", activeItems);
        invenDict.Add("패시브", passiveItems);

        weaponObjs = Resources.LoadAll<GameObject>("03.Junil/Weapon/ItemUseWeapons");


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

        for (int i = 0; i < inventorySetObjs_.transform.childCount; i++)
        {
            GameObject nowEquipment_ = inventorySetObjs_.transform.GetChild(i).gameObject;

            GameObject nowEquipmentInven_ = nowEquipment_.transform.GetChild(1).gameObject;

            GameObject tempInventoryObj_ = nowEquipmentInven_.transform.GetChild(0).gameObject;

            switch (i)
            {
                case 0:
                    gunsInven = tempInventoryObj_;
                    break;

                case 1:
                    activeInven = tempInventoryObj_;
                    break;

                case 2:
                    passiveInven = tempInventoryObj_;
                    break;
            }
        }
    }
}
