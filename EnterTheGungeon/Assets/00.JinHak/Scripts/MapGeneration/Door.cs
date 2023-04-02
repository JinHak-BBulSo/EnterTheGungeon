using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator doorAni = default;
    [SerializeField]
    BoxCollider2D doorCol = default;
    bool isOpen = false;
    void Awake()
    {
        doorAni = GetComponent<Animator>();
        doorCol = GetComponent<BoxCollider2D>();

        DoorManager.Instance.DoorOpen += this.DoorOpen;
        DoorManager.Instance.DoorClose += this.DoorClose;
    }

    public void DoorOpen()
    {
        if (doorCol != null)
        {
            doorCol.isTrigger = true;
        }
    }

    public void DoorClose()
    {
        if (doorCol != null)
        {
            doorCol.isTrigger = false;
        }
        isOpen = false;

        if (doorAni != null)
        {
            doorAni.SetBool("isOpen", false);
            doorAni.SetBool("isClose", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isOpen && PlayerManager.Instance.nowPlayerInRoom.isRoomClear)
        {
            if (doorAni != null)
            {
                SoundManager.Instance.Play("Obj/door_open_01", Sound.SFX);
                doorAni.SetBool("isOpen", true);
                doorAni.SetBool("isClose", false);
                isOpen = true;
            }
        }
    }
}
