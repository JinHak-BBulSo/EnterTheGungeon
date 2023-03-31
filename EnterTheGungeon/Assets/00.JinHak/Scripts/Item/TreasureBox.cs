using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject inBoxItem = default;
    Animator boxAni = default;
    bool isOpen = false;
    bool isPlayerAttach = false;
    int itemIndex = -1;

    void Start()
    {
        itemIndex = Random.Range(0, DropManager.Instance.dropItems.Count);
        inBoxItem = DropManager.Instance.dropItems[itemIndex];
        boxAni = GetComponent<Animator>();
    }

    void Update()
    {
        if(isPlayerAttach && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = true;
            GameObject dropItem_ = Instantiate(inBoxItem, transform.parent);
            inBoxItem.transform.position = transform.position - new Vector3(0, 1, 0);
            dropItem_.transform.parent = PlayerManager.Instance.player.transform.parent.parent;

            boxAni.SetTrigger("isOpen");
            
            if(itemIndex > 2)
            {
                DropManager.Instance.dropItems.RemoveAt(itemIndex);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerAttach = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerAttach = false;
        }
    }
}
