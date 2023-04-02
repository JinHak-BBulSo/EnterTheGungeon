using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    bool isOpen = false;
    BoxCollider2D entranceCollider = default;
    bool isFirstOpen = false;
    public Room bossRoom = default;
    Vector2 originTransform = default;

    void Start()
    {
        entranceCollider = GetComponent<BoxCollider2D>();
        DoorManager.Instance.DoorClose += EntranceClose;
        originTransform = new Vector2(transform.position.x, transform.position.y);
    }

    public void EntranceClose()
    {
        if (isFirstOpen && !bossRoom.isRoomClear)
        {
            StartCoroutine(BossEntranceClose());
        }
    }

    public void EntranceOpen()
    {
        StartCoroutine(BossEntranceOpen());
        entranceCollider.isTrigger = true;
    }
    IEnumerator BossEntranceOpen()
    {
        float timer_ = 0;

        while(true)
        {
            timer_ += Time.deltaTime;
            if(timer_ > 1)
            {
                timer_ = 0;
                yield break;
            }
            else
            {
                transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
                yield return null;
            }
        }
    }

    IEnumerator BossEntranceClose()
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(0.5f);
        entranceCollider.isTrigger = false;
        transform.position = originTransform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isOpen)
        {
            isOpen = true;
            if (!isFirstOpen)
            {
                isFirstOpen = true;
                SoundManager.Instance.Play("Obj/bossdoor_open_01", Sound.SFX);
            }
            StartCoroutine(BossEntranceOpen());
        }
    }
}
