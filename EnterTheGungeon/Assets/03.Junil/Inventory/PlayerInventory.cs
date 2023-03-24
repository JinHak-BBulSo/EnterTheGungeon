using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour
{
    // { [Junil] 인벤토리를 관리할 리스트와 딕셔너리

    private int invenTabMenu = default;

    private const int IEVEN_TAB_VAL = 5;

    //private Dictionary<string, Sprite> inventorySprite = default;

    private Dictionary<string, List<GameObject>> invenDict = default;
    private List<GameObject> weaponItems = default;
    private List<GameObject> activeItems = default;
    private List<GameObject> passiveItems = default;
    // } [Junil] 인벤토리를 관리할 리스트와 딕셔너리

    private const int IEVEN_TAB_ICON_CNT = 4;


    // 인벤토리 탭 종류 | 장비, 총, 아이템, 적, 보스
    private GameObject[] equipmentMenu = default;
    private GameObject[] gunMenu = default;
    private GameObject[] itemMenu = default;
    private GameObject[] monsterMenu = default;
    private GameObject[] bossMenu = default;

    // 장비 - 총에 들어갈 오브젝트 배열
    public GameObject[] weaponObjs = default;


    // 총 아이템을 모아두는 인벤토리
    public GameObject gunsInven = default;

    // 액티브 아이템을 모아두는 인벤토리
    public GameObject activeInven = default;

    // 패시브 아이템을 모아두는 인벤토리
    public GameObject passiveInven = default;


    // 현재 선택한 아이템의 이름을 보여줄 텍스트
    public Text itemNameTxt = default;

    // 아이템이 선택될 때 보여질 그림자 오브젝트
    public GameObject itemTrueImg = default;

    // 현재 선택된 아이템이 나올 오브젝트
    public GameObject itemImg = default;

    // 현재 선택한 아이템의 한 줄 설명을 보여줄 텍스트
    public Text itemDescTxt = default;

    // 현재 선택한 아이템의 역할을 보여줄 텍스트
    public Text itemClassTxt = default;


    // 현재 선택한 아이템의 설명을 보여줄 오브젝트
    public GameObject itemTxtObj = default;

    // 현재 선택한 아이템의 설명을 보여줄 텍스트
    public Text itemTxt = default;


    // 현재 몇 번째 인벤인지 알려주는 int 값
    public int nowTabInvenCnt = default;

    // 현재 선택된 탭이 무엇인지 알려주는 int 값
    public int selectTabInvenVal = default;


    // 현재 탭을 벗어났는지 확인하는 bool 값
    public bool isOutTabMenu = false;


    private void Awake()
    {

        equipmentMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        gunMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        itemMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        monsterMenu = new GameObject[IEVEN_TAB_ICON_CNT];
        bossMenu = new GameObject[IEVEN_TAB_ICON_CNT];


        // 인벤토리에서 수정해야 할 곳들을 캐싱하는 함수
        SetInventory();


        invenTabMenu = IEVEN_TAB_VAL;
        nowTabInvenCnt = 0;
        selectTabInvenVal = nowTabInvenCnt;
        isOutTabMenu = false;

        //Sprite[] sprites_ = Resources.LoadAll<Sprite>("03.Junil/Inventory");


        //// 인벤토리 스프라이트 배열 선언
        //inventorySprite = new Dictionary<string, Sprite>();

        //for(int i = 0; i < sprites_.Length; i++)
        //{
        //    inventorySprite[sprites_[i].name] = sprites_[i];
        //}

        // 현재 탭 int 값을 보고 거기에 맞는 리스트를 가져온다
        invenDict = new Dictionary<string, List<GameObject>>();
        weaponItems = new List<GameObject>();
        activeItems = new List<GameObject>();
        passiveItems = new List<GameObject>();

        invenDict.Add("총", weaponItems);
        invenDict.Add("액티브", activeItems);
        invenDict.Add("패시브", passiveItems);


        weaponObjs = Resources.LoadAll<GameObject>("03.Junil/Weapon/ItemUseWeapons");


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
            selectTabInvenVal = nowTabInvenCnt;
            GFunc.Log("엔터값 입력");
            TabInvenMenuVal(nowTabInvenCnt);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 0~4 까지의 범위를 범어나지 않게 하기 위한 조건
            if (nowTabInvenCnt == 0) { return; }
            GFunc.Log("인벤 작동");

            nowTabInvenCnt--;
            TabInvenMenuVal(nowTabInvenCnt);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 0~4 까지의 범위를 범어나지 않게 하기 위한 조건
            if (nowTabInvenCnt == 4) { return; }
            GFunc.Log("인벤 작동");

            nowTabInvenCnt++;
            TabInvenMenuVal(nowTabInvenCnt);

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

    /// @brief 장비 창을 보여주는 함수
    public void OnEquipmentMenu()
    {

        //foreach(GameObject nowWeapon in weaponObjs)
        //{
        //    invenDict["총"].Add(nowWeapon);
        //}


    }



    /// @brief 인벤토리 탭을 어떤 걸 선택했는지 보여주는 함수
    public void TabInvenMenuVal(int nowTabInven)
    {
        ResetTabInvenMenu(equipmentMenu);
        ResetTabInvenMenu(gunMenu);
        ResetTabInvenMenu(itemMenu);
        ResetTabInvenMenu(monsterMenu);
        ResetTabInvenMenu(bossMenu);

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


    /// @brief 현재 모든 인벤토리 탭의 선택 값을 초기화하는 함수
    public void ResetTabInvenMenu(GameObject[] tabMenu)
    {
        for (int i = 0; i < IEVEN_TAB_ICON_CNT; i++)
        {
            tabMenu[i].SetActive(false);
        }
    }
    
    /// @brief 초기 인벤토리를 셋팅하는 함수
    public void SetInventory()
    {
        GameObject ammonomicMenu_ = gameObject.FindChildObj("AmmonomiconMenu");
        GameObject ammonomicList_ = ammonomicMenu_.FindChildObj("AmmonomiconList");
        GameObject ammonomicInfo_ = ammonomicMenu_.FindChildObj("AmmonomiconInfo");

        GameObject invenMenu_ = ammonomicList_.FindChildObj("InventoryMenu");


        GameObject equipmentMenuChild_ = invenMenu_.FindChildObj("01EquipmentMenu");
        GameObject gunMenuChild_ = invenMenu_.FindChildObj("02GunMenu");
        GameObject itemMenuChild_ = invenMenu_.FindChildObj("03ItemMenu");
        GameObject monsterMenuChild_ = invenMenu_.FindChildObj("04MonsterMenu");
        GameObject bossMenuChild_ = invenMenu_.FindChildObj("05BossMenu");

        for (int i = 0; i < IEVEN_TAB_ICON_CNT; i++)
        {
            equipmentMenu[i] = equipmentMenuChild_.transform.GetChild(i).gameObject;
            gunMenu[i] = gunMenuChild_.transform.GetChild(i).gameObject;
            itemMenu[i] = itemMenuChild_.transform.GetChild(i).gameObject;
            monsterMenu[i] = monsterMenuChild_.transform.GetChild(i).gameObject;
            bossMenu[i] = bossMenuChild_.transform.GetChild(i).gameObject;

            equipmentMenu[i].SetActive(false);
            gunMenu[i].SetActive(false);
            itemMenu[i].SetActive(false);
            monsterMenu[i].SetActive(false);
            bossMenu[i].SetActive(false);
        }

        equipmentMenu[0].SetActive(true);
        gunMenu[3].SetActive(true);
        itemMenu[3].SetActive(true);
        monsterMenu[3].SetActive(true);
        bossMenu[3].SetActive(true);

        GameObject inventorySetObjs_ = ammonomicList_.FindChildObj("InventorySetObjs");

        GameObject nowInvenGun_ = inventorySetObjs_.FindChildObj("02NowInvenGun");
        GameObject gunInvens_ = nowInvenGun_.FindChildObj("GunInvens");


        gunsInven = gunInvens_.FindChildObj("GunsInventory");

        GameObject nowInvenActive_ = inventorySetObjs_.FindChildObj("03NowInvenActive");
        GameObject activeInvens_ = nowInvenActive_.FindChildObj("ActiveInvens");

        activeInven = activeInvens_.FindChildObj("ActiveInventory");

        GameObject nowInvenPassive_ = inventorySetObjs_.FindChildObj("04NowInvenPassive");
        GameObject passiveInvens_ = nowInvenPassive_.FindChildObj("PassiveInvens");

        passiveInven = passiveInvens_.FindChildObj("PassiveInventory");

        GameObject inventoryInfoSetObjs_ = ammonomicInfo_.FindChildObj("InventoryInfoSetObjs");
        GameObject itemNameObj_ = inventoryInfoSetObjs_.FindChildObj("01ItemName");

        itemNameTxt = itemNameObj_.FindChildObj("01ItemNameTxt").GetComponentMust<Text>();

        GameObject itemImgObj_ = inventoryInfoSetObjs_.FindChildObj("02ItemImg");
        GameObject itemImgBase_ = itemImgObj_.FindChildObj("ItemImageBaseObj");

        itemTrueImg = itemImgBase_.FindChildObj("ItemTrueImg");
        itemImg = itemImgBase_.FindChildObj("ItemImg");


        GameObject itemDescObj_ = inventoryInfoSetObjs_.FindChildObj("03ItemDesc");
        GameObject itemDescOneObj_ = itemDescObj_.FindChildObj("ItemDescOneObj");

        itemDescTxt = itemDescOneObj_.FindChildObj("ItemDescTxtOne").GetComponentMust<Text>();

        GameObject itemClassObj_ = inventoryInfoSetObjs_.FindChildObj("04ItemClass");
        GameObject itemClassOneObj_ = itemClassObj_.FindChildObj("ItemClassOneObj");

        itemClassTxt = itemClassOneObj_.FindChildObj("ItemClassTxt").GetComponentMust<Text>();

        GameObject scrollObjs_ = inventoryInfoSetObjs_.FindChildObj("06ScrollObj");
        GameObject scrollView_ = scrollObjs_.FindChildObj("Scroll View");
        GameObject viewport_ = scrollView_.FindChildObj("Viewport");
        GameObject content_ = viewport_.FindChildObj("Content");

        itemTxtObj = content_.FindChildObj("ItemTxt");
        itemTxt = itemTxtObj.GetComponentMust<Text>();
    }
}
