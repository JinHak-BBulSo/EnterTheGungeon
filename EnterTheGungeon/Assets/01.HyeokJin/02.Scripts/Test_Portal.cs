using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Portal : MonoBehaviour
{
    public Vector3 destination;

    private void OnMouseDown()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = destination;
    }
}
