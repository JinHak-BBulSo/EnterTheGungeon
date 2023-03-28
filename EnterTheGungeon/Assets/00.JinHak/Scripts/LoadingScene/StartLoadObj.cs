using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadObj : MonoBehaviour
{
    void Start()
    {
        LoadingManager.Instance.StartLoading(); 
    }
}
