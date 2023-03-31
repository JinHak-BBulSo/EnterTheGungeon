using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    bool isOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isOpen)
        {
            isOpen = true;
            StartCoroutine(EntranceOpen());
        }
    }

    IEnumerator EntranceOpen()
    {
        float timer_ = 0;

        while(true)
        {
            timer_ += Time.deltaTime;
            if(timer_ > 1)
            {
                yield break;
            }
            else
            {
                transform.position += new Vector3(0, 1f * Time.deltaTime, 0);
                yield return null;
            }
        }
    }
}
