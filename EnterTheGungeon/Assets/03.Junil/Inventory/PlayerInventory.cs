using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour
{
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
        // 인벤토리에서 수정해야 할 곳들을 캐싱하는 함수
        SetInventory();



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SetInventory()
    {
        GameObject ammonomicMenu_ = gameObject.FindChildObj("AmmonomiconMenu");
        GameObject ammonomicList_ = ammonomicMenu_.FindChildObj("AmmonomiconList");
        GameObject ammonomicInfo_ = ammonomicMenu_.FindChildObj("AmmonomiconInfo");

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
