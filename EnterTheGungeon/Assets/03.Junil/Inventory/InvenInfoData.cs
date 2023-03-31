using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InvenInfoData : MonoBehaviour
{
    // 현재 선택한 아이템의 이름을 보여줄 텍스트
    public Text itemNameTxt = default;

    // 아이템이 선택될 때 보여질 그림자 오브젝트
    public GameObject itemTrueImg = default;

    // 현재 선택된 아이템 이미지
    public Image itemImg = default;

    // 현재 선택한 아이템의 한 줄 설명을 보여줄 텍스트
    public Text itemDescTxt = default;

    // 현재 선택한 아이템의 역할을 보여줄 텍스트
    public Text itemTypeTxt = default;


    // 현재 선택한 아이템의 설명을 보여줄 텍스트
    public Text itemTxt = default;


    // Start is called before the first frame update
    void Start()
    {

    }


    public void SetInvenInfo()
    {
        // 선택한 아이템 설명을 보여주는 페이지
        GameObject inventoryInfoSetObjs_ = gameObject.transform.GetChild(0).gameObject;

        // 선택한 아이템의 이름        
        itemNameTxt = inventoryInfoSetObjs_.transform.GetChild(0).gameObject.GetComponentMust<Text>();


        // 선택한 아이템 이미지와 그림자
        GameObject itemImgBase_ = inventoryInfoSetObjs_.transform.GetChild(1).gameObject;

        // 그림자 이미지
        itemTrueImg = itemImgBase_.transform.GetChild(1).gameObject;

        // 선택한 아이템 이미지
        itemImg = itemImgBase_.transform.GetChild(2).gameObject.GetComponentMust<Image>();


        // 선택한 아이템의 한 줄 설명
        GameObject itemDescObj_ = inventoryInfoSetObjs_.transform.GetChild(2).gameObject;

        itemDescTxt = itemDescObj_.transform.GetChild(0).gameObject.GetComponentMust<Text>();


        // 선택한 아이템의 역할
        GameObject itemClassObj_ = inventoryInfoSetObjs_.transform.GetChild(3).gameObject;

        itemTypeTxt = itemClassObj_.transform.GetChild(0).gameObject.GetComponentMust<Text>();


        // 선택한 아이템의 상세 설명
        GameObject scrollView_ = inventoryInfoSetObjs_.transform.GetChild(5).gameObject;

        GameObject scrollViewport_ = scrollView_.transform.GetChild(0).gameObject;

        GameObject scrollContent_ = scrollViewport_.transform.GetChild(0).gameObject;

        itemTxt = scrollContent_.transform.GetChild(0).gameObject.GetComponentMust<Text>();
    }
}
