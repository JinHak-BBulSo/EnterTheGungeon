using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    private GameObject miniMap = default;
    [SerializeField] public Camera miniMap_Cam = default;

    private void Awake()
    {
        miniMap = GameObject.Find("MiniMap");
    }

    private void Start()
    {
        miniMap.SetActive(false);
        miniMap_Cam.enabled = false;
    }

    private void Update()
    {
        DisplayMiniMap();
    }

    private void DisplayMiniMap()
    {
        //[yuiver](팀원코드 수정)메뉴 호출시 미니맵이 호출가능한 오류를 수정하기 위해 예외처리로 && Time.timeScale == 1 을 추가했습니다.
        if (Input.GetKey(KeyCode.Tab) && Time.timeScale == 1)
        {
            miniMap.SetActive(true);
            miniMap_Cam.enabled = true;
        }
        else
        {
            miniMap.SetActive(false);
            miniMap_Cam.enabled = false;
        }
    }
}
