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

    private Dictionary<string, Sprite> inventorySprite = default;

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



        Sprite[] sprites_ = Resources.LoadAll<Sprite>("03.Junil/Inventory");


        // 인벤토리 스프라이트 배열 선언
        inventorySprite = new Dictionary<string, Sprite>();

        for(int i = 0; i < sprites_.Length; i++)
        {
            inventorySprite[sprites_[i].name] = sprites_[i];
        }


        invenDict = new Dictionary<string, List<GameObject>>();
        weaponItems = new List<GameObject>();
        activeItems = new List<GameObject>();
        passiveItems = new List<GameObject>();



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// @brief 인벤토리 탭을 어떤 걸 선택했는지 보여주는 함수
    public void ViewTabInven()
    {
        
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
        }



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
