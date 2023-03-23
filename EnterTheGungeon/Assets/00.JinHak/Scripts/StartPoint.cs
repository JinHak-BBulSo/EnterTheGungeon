using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public void SetPlayer()
    {
        GameObject.FindObjectOfType<PlayerController>().transform.position =
            transform.position + new Vector3(-4, 0, 0);
    }
}
