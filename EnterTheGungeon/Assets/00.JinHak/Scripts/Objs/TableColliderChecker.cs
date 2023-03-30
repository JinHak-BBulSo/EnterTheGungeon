using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableColliderChecker : MonoBehaviour
{
    private Image image = default;
    private Table table = default;
    [SerializeField]
    private OverDistance distance = OverDistance.NONE;

    void Start()
    {
        table = transform.parent.parent.GetComponent<Table>();
        image = GetComponent<Image>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !table.isOver)
        {
            image.color = new Color(1, 1, 1, 1);
            table.distance = distance;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            image.color = new Color(1, 1, 1, 0);
            if(!table.isOver)
                table.distance = OverDistance.NONE;
        }
    }
}
