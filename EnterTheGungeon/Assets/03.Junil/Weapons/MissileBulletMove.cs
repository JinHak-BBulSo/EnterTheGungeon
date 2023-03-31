using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MissileBulletMove : PlayerBullet
{

    // 미사일 오브젝트의 리지드바디
    private Rigidbody2D missileBulletRigid2D = default;

    private Animator missileAni = default;

    public Vector3 activePos = default;

    public bool isOffBullet = false;

    public GameObject targetMonster = default;

    public float radiusLength = default;


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

        targetMonster = default;
    }

    // Start is called before the first frame update
    void Start()
    {
        isOffBullet = false;
        radiusLength = 5f;

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

        // 몬스터를 감지하면 유도되는 조건
        if(targetMonster != null || targetMonster != default)
        {

        }

        // 미사일이 일정 거리 만큼 날아가면 원운동이 작동하는 조건
        if(radiusLength < Len_)
        {
            StartRotateMissile();
        }



    }

    IEnumerator RotateMissile = default;

    void StartRotateMissile()
    {
        RotateMissile = RotateMissileMove();
        StartCoroutine(RotateMissile);
    }

    void StopRotateMissile()
    {
        if(RotateMissile != null)
        {
            StopCoroutine(RotateMissile);
        }
    }

    IEnumerator RotateMissileMove()
    {
        GFunc.Log("회전 실행돠ㅣㅁ");
        float angle_ = 45f;
        int countLoop_ = 0;

        while (true)
        {
            if(countLoop_ == 8)
            {
                countLoop_ = 0;
            }


            Vector3 movePos_ = new Vector3(
                radiusLength * Mathf.Cos(angle_ * Mathf.Deg2Rad), radiusLength * Mathf.Sin(angle_ * Mathf.Deg2Rad), 0f);

            gameObject.transform.position += movePos_;

            countLoop_++;

            yield return new WaitForSeconds(5f);
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
