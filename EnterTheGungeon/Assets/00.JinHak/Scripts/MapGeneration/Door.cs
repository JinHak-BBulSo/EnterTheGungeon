using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAni = default;
    BoxCollider2D doorCol = default;
    bool isOpen = false;
    void Start()
    {
        doorAni = GetComponent<Animator>();
        doorCol = GetComponent<BoxCollider2D>();

        DoorManager.Instance.DoorOpen += this.DoorOpen;
        DoorManager.Instance.DoorClose += this.DoorClose;
    }

    public void DoorOpen()
    {
        doorCol.isTrigger = true;
    }

    public void DoorClose()
    {
        doorCol.isTrigger = false;
        isOpen = false;
        doorAni.SetTrigger("isClose");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isOpen)
        {
            doorAni.SetTrigger("isOpen");
            isOpen = true;
        }
    }
}
