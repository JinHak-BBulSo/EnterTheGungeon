using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKingController : MonoBehaviour
{
    private ObjectManager objectManager = default;
    private GameObject player = default;

    private float bulletCount = default;
    private float bulletSpeed = default;
    private float bulletLifeTime = default;
    private float moveSpeed = default;
    private float enemyRadius = default;

    private int health = default;
    private int patternIndex = default;
    private int curPatternCount = default;
    private int maxPatternCount = default;
    private int bulletGap = default;


    private bool isPatterenCountReset = false;
    private bool isMoving = false;


    private float distance = default;

    private float direction_X = default;
    private float direction_Y = default;


    private void OnEnable()
    {
        health = 950;
        Invoke("Test", 1f);
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }
    private void Start()
    {

    }

    private void Update()
    {

    }

    private void FindPlayer()
    {
        float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x);
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

    private void Pattern()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex + 1)
        {
            case 1:
                Pattern_1();
                break;
            case 2:
                Pattern_2();
                break;
            case 3:
                Pattern_3();
                break;
            case 4:
                Pattern_4();
                break;
            case 5:
                Pattern_5();
                break;
            case 6:
                Pattern_6();
                break;
        }

    }

    #region Pattern_1
    //  [YHJ] 2023-03-16
    //  @brief Bullet King 전방, 후방을 제외한 위치에 플레이어가 있을 시 플레이어를 향해 3-Way 총알을 두 번 발사한다.
    private void Pattern_1()
    {
        //Debug.Log($"{direction_X}, {direction_Y}");

        //direction_X = transform.position.x - player.transform.position.x;
        //direction_Y = transform.position.y - player.transform.position.y;

        if (direction_X > 1 && direction_Y > 0)
        {
            //  왼쪽 아래로 3-way 총알을 두 번 발사
        }
        if (direction_X < -1 && direction_Y > 0)
        {
            //  오른쪽 아래로 3-way 총알을 두 번 발사
        }

        //GameObject bulletR = objectManager.MakeObject("EnemyBullet");
        //bulletR.transform.position = new Vector2(transform.position.x -1f,transform.position.y -1f); ;

        //Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        //rigidR.velocity = new Vector2(player.transform.position.x, player.transform.position.y) * 3f;

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_1", 1f);
        }
        else
        {
            Invoke("Pattern", 2f);
        }
    }   //  Pattern_1()
    #endregion

    #region Pattern_2
    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 나아가는 총알을 4겹 원형으로 발사한다.
    private void Pattern_2()
    {
        maxPatternCount = 4;
        bulletSpeed = 10f;
        bulletCount = 36;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObject("Bullet_TypeA");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
            bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_2", 0.5f);
        }
    }   //  Pattern_2()
    #endregion

    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 퍼져 나가는 깜빡거리는 총알을 여러 개의 일직선으로 6개 발사한다.
    private void Pattern_3()
    {
        maxPatternCount = 6;
        bulletSpeed = 6f;
        bulletCount = 40;
        bulletGap = 4;
        enemyRadius = 1.8f;

        for (int i = 0; i < bulletCount; i ++)
        {
            if (i % bulletGap == 0)
            {
                /* Do Nothing */
            }
            else
            {
                GameObject bullet = objectManager.MakeObject("Bullet_TypeB");
                bullet.transform.position = transform.position + new Vector3(Mathf.Cos(Mathf.PI * 2 * i / (bulletCount)), Mathf.Sin(Mathf.PI * 2 * i / (bulletCount))) * enemyRadius;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / (bulletCount)), Mathf.Sin(Mathf.PI * 2 * i / (bulletCount)));
                bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
            }
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_3", 0.1f);
        }
        else
        {
            Invoke("Pattern", 3f);
        }
    }

    //  [YHJ] 2023-03-16
    //  @brief 빙글빙글 돌면서 방 전체에 총알을 일정하게 연사한 뒤, 마지막으로 약간 더 빠른 총알을 자신을 중심으로 원형으로 발사한다.
    private void Pattern_4()
    {
        maxPatternCount = 30;
        bulletSpeed = 8f;
        bulletCount = 10;
        enemyRadius = 1.5f;

        for (int i = 0; i < bulletCount; i++)
        {
            if (curPatternCount % 2 == 0)
            {
                GameObject bullet = objectManager.MakeObject("Bullet_Basic");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / (bulletCount)), Mathf.Sin(Mathf.PI * 2 * i / (bulletCount)));
                bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
            }
            else if (curPatternCount == maxPatternCount - 1)
            {
                GameObject bullet = objectManager.MakeObject("Bullet_TypeB");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
                bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
            }
            else
            {
                GameObject bullet = objectManager.MakeObject("Bullet_Basic");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / (bulletCount)), Mathf.Sin(Mathf.PI * 2 * i / (bulletCount)));
                direction = Quaternion.Euler(0f, 0f, 60) * direction;
                bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
            }
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_4", 0.2f);
        }
    }

    //  [YHJ] 2023-03-16
    //  @brief 자신의 위쪽에 커다란 총알을 한 발 발사한다. 발사된 총알은 여러 개의 깜빡거리는 탄환 여러 개로 나뉘고 이 탄환들은 잠시 뒤 여러 갈래로 퍼져나간다.
    IEnumerator Pattern_5()
    {
        maxPatternCount = 1;
        bulletSpeed = 0.7f;
        bulletCount = 1;

        GameObject bullet = objectManager.MakeObject("Bullet_TypeC");
        bullet.transform.position = transform.position + new Vector3(0f, 2f, 0f);
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = Vector2.up;
        bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);

        bullet.SetActive(false);

        StartCoroutine(Pattern_5_ExplodeBullet_1(bullet.transform.position));

        yield return new WaitForSeconds(0.5f);

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_5", 0.7f);
        }
    }

    IEnumerator Pattern_5_ExplodeBullet_1(Vector3 currentPosition_)
    {
        List<GameObject> bullets = new List<GameObject>();

        maxPatternCount = 1;
        bulletSpeed = 8f;
        bulletCount = 8;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObject("Bullet_TypeB");
            bullet.transform.position = currentPosition_;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
            bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

            bullets.Add(bullet);
        }

        yield return new WaitForSeconds(0.5f);

        foreach (var bullet in bullets)
        {
            StartCoroutine(Pattern_5_ExplodeBullet_2(bullet.transform.position));
            bullet.gameObject.SetActive(false);
        }
    }

    IEnumerator Pattern_5_ExplodeBullet_2(Vector3 nextPosition_)
    {
        List<GameObject> bullets = new List<GameObject>();

        maxPatternCount = 1;
        bulletSpeed = 7f;
        bulletCount = 7;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObject("Bullet_TypeB");
            bullet.transform.position = nextPosition_;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
            bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

            bullets.Add(bullet);
        }

        yield return new WaitForSeconds(0.5f);

        foreach (var bullet in bullets)
        {
            StartCoroutine(Pattern_5_ExplodeBullet_3(bullet.transform.position));
            bullet.gameObject.SetActive(false);
        }
    }

    IEnumerator Pattern_5_ExplodeBullet_3(Vector3 next2Position_)
    {
        List<GameObject> bullets = new List<GameObject>();

        maxPatternCount = 1;
        bulletSpeed = 6f;
        bulletCount = 6;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObject("Bullet_TypeB");
            bullet.transform.position = next2Position_;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
            bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

            bullets.Add(bullet);
        }

        yield return null;
    }

    private void Test()
    {
        StartCoroutine("Pattern_5");
    }

    //  [YHJ] 2023-03-16
    //  @brief 플레이어를 향해 화염병처럼 화염 장판을 원형으로 까는 술잔을 던진다.
    private void Pattern_6()
    {
        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_6", 0.7f);
        }
    }
}
