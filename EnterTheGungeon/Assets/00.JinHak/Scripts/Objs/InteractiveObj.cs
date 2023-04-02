using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObj : MonoBehaviour
{
    protected Animator objAni = default;
    /*public List<GameObject> shardPrefabs = default;
    public List<Rigidbody2D> shardRigid = default;*/
    protected virtual void Start()
    {
        objAni = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "PlayerBullet" || collision.tag == "MonsterBullet")
        {
            objAni.SetTrigger("Broken");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }    
    }
}
