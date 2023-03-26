using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public Item item = default;
    protected bool isGetAble = false;

    void Update()
    {
        if (isGetAble && Input.GetKeyDown(KeyCode.E))
        {
            GetDropItem();
            gameObject.SetActive(false);
        }
    }

    public virtual void GetDropItem()
    {
        /* Override Using */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isGetAble = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isGetAble = false;
        }
    }
}
