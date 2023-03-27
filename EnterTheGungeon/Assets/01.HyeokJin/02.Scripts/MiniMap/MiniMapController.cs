using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    private GameObject miniMap = default;

    private void Awake()
    {
        miniMap = GameObject.Find("MiniMap");
    }

    private void Start()
    {
        miniMap.SetActive(false);
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
        }
        else
        {
            miniMap.SetActive(false);
        }

    }
}
