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
            Vector2 forceVector = transform.position - collision.transform.position;
            objAni.SetTrigger("Broken");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            /*foreach (GameObject shard in shardPrefabs)
            {
                GameObject createShard = Instantiate(shard, transform.position, Quaternion.identity);
                createShard.transform.parent = transform;
                forceVector = new Vector2(forceVector.x * Random.Range(0.3f, 1f), forceVector.y * Random.Range(0.3f, 1f));
                Debug.Log(forceVector.normalized);
                createShard.transform.GetChild(0).GetComponent<Rigidbody2D>().AddForce(forceVector.normalized * 150);
                shardRigid.Add(createShard.transform.GetChild(0).GetComponent<Rigidbody2D>());
            }
            StartCoroutine(Stop());*/
        }    
    }

    /*IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.5f);
        foreach(var rigid in shardRigid)
        {
            rigid.velocity = Vector2.zero;
        }
    }*/
}
