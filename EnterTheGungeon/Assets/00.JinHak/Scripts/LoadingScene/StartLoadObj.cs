using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartLoadObj : MonoBehaviour
{
    void Start()
    {
        LoadingManager.Instance.StartLoading();
    }
}
