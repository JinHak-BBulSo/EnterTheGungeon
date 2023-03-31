using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffMiniMap : MonoBehaviour
{
    //! UI 호출시에 카메라 지정이 안되있는 03.Scene의 UIObjs 하위의 미니맵이 UI호출시에도 최상단에 보이는 예외처리를 위해 추가  
    public GameObject miniCam = default;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            miniCam.SetActive(false);
        }
        if (Time.timeScale == 1 && miniCam.activeSelf == false)
        {
            miniCam.SetActive(true);
        }
    }
}
