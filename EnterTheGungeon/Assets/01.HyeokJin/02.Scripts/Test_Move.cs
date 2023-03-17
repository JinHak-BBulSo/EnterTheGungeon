using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Move : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;

    public void MoveTo(Vector3 direction_)
    {
        moveDirection = direction_;
    }
}
