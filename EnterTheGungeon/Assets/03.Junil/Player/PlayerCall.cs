using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCall : MonoBehaviour
{

    private void Awake()
    {
        PlayerManager.Instance.Create();
        InventoryManager.Instance.Create();

        GFunc.Log("각 매니저 생성");

    }

}
