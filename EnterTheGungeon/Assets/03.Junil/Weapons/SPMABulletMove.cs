using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPMABulletMove : PlayerBullet
{

    private Rigidbody2D spmaBulletRigid2D = default;


    // Start is called before the first frame update
    void Start()
    {

        spmaBulletRigid2D = gameObject.GetComponentMust<Rigidbody2D>();

        bulletSpeed = 6f;

        spmaBulletRigid2D.velocity = transform.up * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public override void OnOffBullet()
    {
        base.OnOffBullet();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            spmaBulletRigid2D.velocity = Vector2.zero;

            OnOffBullet();
        }
    }
}
