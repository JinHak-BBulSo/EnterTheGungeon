using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class BulletKingController : MonoBehaviour
{
    [SerializeField] public Transform target = default;
    [SerializeField] public GameObject bulletPrefab = default;

    private List<GameObject> bullets = default;
    private Rigidbody2D bulletkingRigidbody = default;
    private Animator bulletkingAnimator = default;

    private Test_Move test_Move = default;

    private float distance = default;
    private float moveSpeed = default;

    private float direction_X = default;
    private float direction_Y = default;

    private float intervalAngle = default;
    private float weightAngle = default;

    private float bulletSpeed = default;
    private float bulletInterval = default;
    private float bulletCount = default;

    private bool isMoving = false;


    private void Awake()
    {
        bulletkingRigidbody = GetComponent<Rigidbody2D>();
        bulletkingAnimator = GetComponent<Animator>();
        bullets = new List<GameObject>();

        test_Move = GetComponent<Test_Move>();

        moveSpeed = 0.2f;
    }
    private void Start()
    {

    }

    private void Update()
    {

    }

    //  [YHJ] 2023-03-16
    //  @brief Bullet King 이 Player를 추적한다.
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

    //  [YHJ] 2023-03-16
    //  @brief Bullet King 전방, 후방을 제외한 위치에 플레이어가 있을 시 플레이어를 향해 3-Way 총알을 두 번 발사한다.
    private void Pattern_1()
    {
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

    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 나아가는 36개의 총알을 1겹 원형으로 발사한다.
    private void Pattern_2()
    {
        bulletCount = 36;
        bulletSpeed = 0.5f;
        intervalAngle = 360 / bulletCount;
        weightAngle = 0;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.3f), Quaternion.identity);
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI) * 2 * i / (bulletCount - 1), Mathf.Sin(Mathf.PI) * i * 2 / (bulletCount - 1));
            bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed);
        }



        //for (int i = 0; i < bulletCount; i++)
        //{
        //    //  발사체 생성
        //    GameObject clone = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //    //  발사체 이동 방향 (각도)
        //    float angle = weightAngle + intervalAngle * i;

        //    //  발사체 이동 방향 (벡터)
        //    float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
        //    float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

        //}

        //weightAngle += 1;

    }

    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 퍼져 나가는 깜빡거리는 총알을 여러 개의 일직선으로 4개 발사한다.
    private void Pattern_3()
    {

    }

    //  [YHJ] 2023-03-16
    //  @brief 빙글빙글 돌면서 방 전체에 총알을 일정하게 연사한 뒤, 마지막으로 약간 더 빠른 총알을 자신을 중심으로 원형으로 발사한다.
    private void Pattern_4()
    {

    }

    //  [YHJ] 2023-03-16
    //  @brief 자신의 위쪽에 커다란 총알을 한 발 발사한다. 발사된 총알은 여러 개의 깜빡거리는 탄환 여러 개로 나뉘고 이 탄환들은 잠시 뒤 여러 갈래로 퍼져나간다.
    private void Pattern_5()
    {

    }

    //  [YHJ] 2023-03-16
    //  @brief 플레이어를 향해 화염병처럼 화염 장판을 원형으로 까는 술잔을 던진다.
    private void Pattern_6()
    {

    }
}
