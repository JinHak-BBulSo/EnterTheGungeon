using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    CircleCollider2D shardCol;
    Rigidbody2D shardRigid;
    void Start()
    {
        shardCol = GetComponent<CircleCollider2D>();
        shardRigid = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            shardCol.isTrigger = false; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            shardCol.isTrigger = true;
        }
    }
}
