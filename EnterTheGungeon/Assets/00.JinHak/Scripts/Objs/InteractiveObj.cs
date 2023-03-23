using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : MonoBehaviour
{
    protected Animator objAni = default;
    protected virtual void Start()
    {
        objAni = GetComponent<Animator>();
    }
}
