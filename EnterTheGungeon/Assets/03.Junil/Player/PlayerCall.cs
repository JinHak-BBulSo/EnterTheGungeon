using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCall : MonoBehaviour
{

    private void Awake()
    {
        PlayerManager.Instance.Create();
        InventoryManager.Instance.Create();
        GFunc.Log($"Create ok");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
