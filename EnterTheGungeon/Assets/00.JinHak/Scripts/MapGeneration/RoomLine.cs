using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" && collision.transform.parent.name != "MapAccessWall")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
