using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletKingController : MonoBehaviour
{
    private Rigidbody2D bulletkingRigidbody = default;
    private Animator bulletkingAnimator = default;
    [SerializeField] public Transform target = default;

    private float distance = default;
    private float moveSpeed = default;

    private float direction_X = default;
    private float direction_Y = default;

    private float bulletSpeed = default;
    private float bulletInterval = default;
    private float bulletCount = default;

    private bool isMoving = false;


    private void Awake()
    {
        bulletkingRigidbody = GetComponent<Rigidbody2D>();
        bulletkingAnimator = GetComponent<Animator>();

        moveSpeed = 0.2f;
    }
    private void Start()
    {

    }

    private void Update()
    {
        Move();

        Pattern_1();
    }

    private void Move()
    {
        distance = Vector2.Distance(transform.localPosition, GameObject.FindWithTag("Player").transform.localPosition);

        if (distance > 300)
        {
            isMoving = true;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.FindWithTag("Player").transform.localPosition, moveSpeed);
        }
        else
        {
            isMoving = false;
        }
    }

    private void Pattern_1()
    {
        //  Bullet King의 전방, 후방을 제외한 위치에 플레이어가 있을 시 플레이어를 향해 3-way 총알을 두 번 발사한다.

        direction_X = transform.position.x - target.position.x;
        direction_Y = transform.position.y - target.position.y;

        if (direction_X > 1 && direction_Y > 0)
        {
            //  왼쪽 아래로 3-way 총알을 두 번 발사
        }
        if (direction_X < -1 && direction_Y > 0)
        {
            //  오른쪽 아래로 3-way 총알을 두 번 발사
        }
    }

    private void Pattern_2()
    {
        //  자신을 중심으로 나아가는 불꽃 모양의 탄을 1겹 원형으로 발사한다. (36개)
    }

    private void Pattern_3()
    {
        //  자신을 중심으로 퍼져 나가는 깜빡거리는 총알을 여러 개의 일직선으로 4개 발사한다
    }

    private void Pattern_4()
    {
        //  빙글빙글 돌면서 방 전체에 총알을 일정하게 연사한 뒤, 마지막으로 약간 더 빠른 총알을 자신을 중심으로 원형으로 발사한다.
    }

    private void Pattern_5()
    {
        //  자신의 위쪽에 커다란 총알을 한 발 발사한다. 발사된 총알은 여러 개의 깜빡거리는 탄환 여러 개로 나뉘고 이 탄환들을 잠시 뒤 여러 갈래로 퍼져나간다.
    }

    private void Pattern_6()
    {
        //  플레이어를 향해 화염병처럼 화염 장판을 원형으로 까는 술잔을 던진다.
    }
}
