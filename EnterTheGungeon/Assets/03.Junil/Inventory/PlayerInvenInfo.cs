using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInvenInfo : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //! 아이템 정보를 셋팅하는 함수 | 추후 수정 예정
    public void SetItemInformation(string itemName_)
    {
        // 아이템 이름
        itemNameTxt.text = itemName_;

        // 아이템 이미지, 그림자 켜기
    }

    public void SetInvenInfo()
    {
        // 선택한 아이템 설명을 보여주는 페이지
        GameObject inventoryInfoSetObjs_ = gameObject.transform.GetChild(0).gameObject;

        // 선택한 아이템의 이름
        GameObject itemNameObj_ = inventoryInfoSetObjs_.transform.GetChild(0).gameObject;

        itemNameTxt = itemNameObj_.transform.GetChild(0).gameObject.GetComponentMust<Text>();


        // 선택한 아이템 이미지와 그림자
        GameObject itemImgObj_ = inventoryInfoSetObjs_.transform.GetChild(1).gameObject;
        GameObject itemImgBase_ = itemImgObj_.transform.GetChild(0).gameObject;

        // 그림자 이미지
        itemTrueImg = itemImgBase_.transform.GetChild(1).gameObject;

        // 선택한 아이템 이미지
        itemImg = itemImgBase_.transform.GetChild(2).gameObject;

        // 선택한 아이템의 한 줄 설명
        GameObject itemDescObj_ = inventoryInfoSetObjs_.transform.GetChild(2).gameObject;

        GameObject itemDescOne_ = itemDescObj_.transform.GetChild(0).gameObject;

        itemDescTxt = itemDescOne_.transform.GetChild(0).gameObject.GetComponentMust<Text>();


        // 선택한 아이템의 역할
        GameObject itemClassObj_ = inventoryInfoSetObjs_.transform.GetChild(3).gameObject;

        GameObject itemClassOne_ = itemClassObj_.transform.GetChild(0).gameObject;

        itemClassTxt = itemClassOne_.transform.GetChild(0).gameObject.GetComponentMust<Text>();


        // 선택한 아이템의 상세 설명
        GameObject scrollObjs_ = inventoryInfoSetObjs_.transform.GetChild(5).gameObject;

        GameObject scrollView_ = scrollObjs_.transform.GetChild(0).gameObject;

        GameObject scrollViewport_ = scrollView_.transform.GetChild(0).gameObject;

        GameObject scrollContent_ = scrollViewport_.transform.GetChild(0).gameObject;

        itemTxtObj = scrollContent_.transform.GetChild(0).gameObject;

        itemTxt = itemTxtObj.GetComponentMust<Text>();
    }
}
