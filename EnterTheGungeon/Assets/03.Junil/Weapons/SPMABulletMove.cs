using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPMABulletMove : PlayerBullet
{

    private Rigidbody2D spmaBulletRigid2D = default;
    private Animator spmaAni = default;

    public Weapons weapons = default;

    public Vector3 activePos = default;

    public bool isOffBullet = false;

    private void OnEnable()
    {
        activePos = gameObject.transform.position;

        if (isOffBullet == true)
        {
            isOffBullet = false;
            spmaAni.SetBool("isOffBullet", isOffBullet);

        }
    }

    private void OnDisable()
    {
        activePos = Vector3.zero;

    }

    // Start is called before the first frame update
    void Start()
    {
        weapons = new MarineNorWeapon();

        isOffBullet = false;

        spmaBulletRigid2D = gameObject.GetComponentMust<Rigidbody2D>();
        spmaAni = gameObject.GetComponentMust<Animator>();

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


    public override IEnumerator OffBullet()
    {
        spmaBulletRigid2D.velocity = Vector2.zero;

        isOffBullet = true;

        spmaAni.SetBool("isOffBullet", isOffBullet);

        yield return new WaitForSeconds(0.4f);

        this.gameObject.SetActive(false);

    }


}
