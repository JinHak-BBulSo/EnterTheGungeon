using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    PlayerController player = default;
    public int price = 0;
    bool isPurchaseAble = false;
    GameObject priceObjs = default;

    private void Start()
    {
        player = PlayerManager.Instance.player;
        priceObjs = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(isPurchaseAble && player.playerMoney >= price && Input.GetKeyDown(KeyCode.E))
        {
            PurchaseItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPurchaseAble = true;
            priceObjs.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPurchaseAble = false;
            priceObjs.SetActive(false);
        }
    }

    protected virtual void PurchaseItem()
    {
        player.playerMoney -= price;
        player.cashController.SetPlayerCash(player.playerMoney);

        GetComponent<DropItem>().GetDropItem();
        gameObject.SetActive(false);
    }
}
