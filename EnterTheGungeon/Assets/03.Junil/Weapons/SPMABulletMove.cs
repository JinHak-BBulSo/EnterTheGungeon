using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPMABulletMove : PlayerBullet
{

    private Rigidbody2D spmaBulletRigid2D = default;

    public Weapons weapons = default;

    public Vector3 activePos = default;

    private void OnEnable()
    {
        activePos = gameObject.transform.position;

    }

    private void OnDisable()
    {
        activePos = Vector3.zero;

    }

    // Start is called before the first frame update
    void Start()
    {
        weapons = new MarineNorWeapon();

        spmaBulletRigid2D = gameObject.GetComponentMust<Rigidbody2D>();

        SetBulletData(weapons);

    }

    // Update is called once per frame
    void Update()
    {
        spmaBulletRigid2D.velocity = transform.up * bulletSpeed;

        float Len_ = Vector3.Distance(gameObject.transform.position, activePos);


        // 처음 발사한 위치에서 일정 거리 가면 발동
        if(bulletRange <= Len_)
        {
            OnOffBullet();
        }
    }



    public override void OnOffBullet()
    {
        spmaBulletRigid2D.velocity = Vector2.zero;
        base.OnOffBullet();

    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {

            this.OnOffBullet();
        }
    }
}
