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
        if (Input.GetKey(KeyCode.Tab))
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
