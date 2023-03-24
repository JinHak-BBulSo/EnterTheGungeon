using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : MonoBehaviour
{
    protected GameObject objRenderer = default;
    void Start()
    {
        objRenderer = transform.GetChild(0).gameObject;
    }
}
