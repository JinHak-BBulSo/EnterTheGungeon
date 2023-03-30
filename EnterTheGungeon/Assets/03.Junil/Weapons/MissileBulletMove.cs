using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBulletMove : PlayerBullet
{

    // 미사일 오브젝트의 리지드바디
    private Rigidbody2D missileBulletRigid2D = default;

    private Animator missileAni = default;

    public Vector3 activePos = default;

    public bool isOffBullet = false;



    private void Awake()
    {
        
        missileBulletRigid2D = gameObject.GetComponentMust<Rigidbody2D>();

        missileAni = gameObject.GetComponentMust<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        activePos = gameObject.transform.position;
        // KJH ADD
        missileBulletRigid2D.velocity = transform.up * bulletSpeed;

        

        if (isOffBullet == true)
        {
            isOffBullet = false;
            missileAni.SetBool("isOffBullet", isOffBullet);

        }
    }

    private void OnDisable()
    {
        activePos = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOffBullet = false;
                
        //KJH 위치 변경 Update -> Start
        missileBulletRigid2D.velocity = transform.up * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {


        float Len_ = Vector3.Distance(gameObject.transform.position, activePos);


        // 처음 발사한 위치에서 일정 거리 가면 발동
        if (bulletRange <= Len_ && !isOffBullet)
        {
            StartCoroutine(OffBullet());
        }


    }


    //! 총알이 총구에서 발사될 때 시작 지점을 지정해주는 함수
    public void SetActivePos()
    {
        activePos = gameObject.transform.position;

    }

    public override IEnumerator OffBullet()
    {
        missileBulletRigid2D.velocity = Vector2.zero;

        isOffBullet = true;

        missileAni.SetBool("isOffBullet", isOffBullet);

        yield return new WaitForSeconds(0.2f);

        this.gameObject.SetActive(false);

    }
}
