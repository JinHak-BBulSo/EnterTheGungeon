using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : InteractiveObj
{
    BoxCollider2D deskBoxCollider = default;
    void Start()
    {
        deskBoxCollider = objRenderer.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
