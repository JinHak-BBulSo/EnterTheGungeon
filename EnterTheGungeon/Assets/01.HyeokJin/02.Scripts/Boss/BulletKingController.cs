using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKingController : MonoBehaviour
{
    private ObjectManager objectManager = default;
    private Animator bulletkingAnimator = default;
    private GameObject player = default;

    private SpriteRenderer throneSpriteRenderer = default;

    private GameObject muzzle_Left_1 = default;
    private GameObject muzzle_Left_2 = default;
    private GameObject muzzle_Left_3 = default;
    private GameObject muzzle_Left_4 = default;
    private GameObject muzzle_Left_5 = default;
    private GameObject muzzle_Right_1 = default;
    private GameObject muzzle_Right_2 = default;
    private GameObject muzzle_Right_3 = default;
    private GameObject muzzle_Right_4 = default;
    private GameObject muzzle_Right_5 = default;


    private float bulletCount = default;
    private float bulletSpeed = default;
    private float moveSpeed = default;
    private float enemyRadius = default;

    private int health = default;
    private int patternIndex = default;
    private int curPatternCount = default;
    private int maxPatternCount = default;
    private int bulletGap = default;


    private bool isPatterenCountReset = false;
    private bool isMoving = false;
    private bool isPatternStart = false;


    private float distance = default;

    private float direction_X = default;
    private float direction_Y = default;


    private void OnEnable()
    {
        health = 950;
        Invoke("Status", 1f);
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        muzzle_Left_1 = GameObject.Find("Muzzle_Left_1");
        muzzle_Left_2 = GameObject.Find("Muzzle_Left_2");
        muzzle_Left_3 = GameObject.Find("Muzzle_Left_3");
        muzzle_Left_4 = GameObject.Find("Muzzle_Left_4");
        muzzle_Left_5 = GameObject.Find("Muzzle_Left_5");
        muzzle_Right_1 = GameObject.Find("Muzzle_Right_1");
        muzzle_Right_2 = GameObject.Find("Muzzle_Right_2");
        muzzle_Right_3 = GameObject.Find("Muzzle_Right_3");
        muzzle_Right_4 = GameObject.Find("Muzzle_Right_4");
        muzzle_Right_5 = GameObject.Find("Muzzle_Right_5");

        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        bulletkingAnimator = GameObject.Find("BulletKing").GetComponent<Animator>();

        throneSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {

    }

    private void Update()
    {
        Move();
    }

    #region Move
    //  [YHJ] 2023-03-16
    //  @brief Bullet King 이 Player를 추적한다.
    private void Move()
    {
        //if (isPatternStart)
        //{
        //    isMoving = false;
        //    return;
        //}

        moveSpeed = 1.5f;

        distance = Vector2.Distance(transform.position, GameObject.FindWithTag("Player").transform.position);

        if (distance > 5)
        {
            isMoving = true;
            Vector3 direction = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
            GameObject.Find("Boss_BulletKing").transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            isMoving = false;
        }
    }   //  Move()
    #endregion

    #region Status
    private void Status()
    {
        patternIndex = Random.Range(6, 7);

        curPatternCount = 0;

        switch (patternIndex)
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
                Test();
                break;
            case 6:
                Pattern_6();
                break;
        }
    }   //  Status
    #endregion

    #region Pattern_1
    //  [YHJ] 2023-03-16
    //  @brief Bullet King 전방, 후방을 제외한 위치에 플레이어가 있을 시 플레이어를 향해 3-Way 총알을 두 번 발사한다.
    private void Pattern_1()
    {
        StartCoroutine("Pattern_1_3way");
    }   //  Pattern_1()

    IEnumerator Pattern_1_3way()
    {
        isPatternStart = true;

        maxPatternCount = 1;
        bulletSpeed = 10f;
        bulletCount = 3;

        direction_X = player.transform.position.x - transform.position.x;
        direction_Y = player.transform.position.y - transform.position.y;

        if (-8 < direction_X && direction_X < -1)
        {
            if (-8 < direction_Y && direction_Y < -1.5)
            {
                //  Left Bottom
                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Left_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Left_4.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Left_5.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, -130f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = new Vector2(-1f, -1f);
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.05f);

                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Left_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Left_4.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Left_5.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, -130f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = new Vector2(-1f, -1f);
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (-1.5 < direction_Y && direction_Y < 0)
            {
                //  Left Center
                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Left_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Left_2.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Left_1.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = Vector2.left;
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.05f);

                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Left_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Left_2.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Left_1.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, 180f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = Vector2.left;
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else if (1 < direction_X && direction_X < 8)
        {
            if (-8 < direction_Y && direction_Y < -1.5)
            {
                //  Right Bottom
                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Right_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Right_4.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Right_5.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, -50f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = new Vector2(1f, -1f);
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.05f);

                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Right_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Right_4.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Right_5.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, -50f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = new Vector2(1f, -1f);
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (-1.5 < direction_Y && direction_Y < 0)
            {
                Debug.Log("Right Center");

                //  Right Center
                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Right_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Right_2.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Right_1.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = Vector2.right;
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.05f);

                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject bullet = objectManager.MakeObject("Bullet_TypeE");

                    switch (i)
                    {
                        case 0:
                            bullet.transform.position = muzzle_Right_3.transform.position;
                            break;
                        case 1:
                            bullet.transform.position = muzzle_Right_2.transform.position;
                            break;
                        case 2:
                            bullet.transform.position = muzzle_Right_1.transform.position;
                            break;
                    }

                    bullet.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    Vector2 direction = Vector2.right;
                    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else
        {
            Debug.Log("Out");
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_1", 0.05f);
        }
        else
        {
            Invoke("Status", 1.5f);
        }
    }   //  Pattern_1()
    #endregion

    #region Pattern_2
    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 나아가는 총알을 4겹 원형으로 발사한다.
    private void Pattern_2()
    {

        isPatternStart = true;
        bulletkingAnimator.SetTrigger("isPattern_2");

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
        else
        {
            Invoke("Pattern", 2f);
        }

        if (curPatternCount > 1)
        {
            isPatternStart = false;
            bulletkingAnimator.ResetTrigger("isPattern_2");
        }
    }   //  Pattern_2()
    #endregion

    #region Pattern_3
    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 퍼져 나가는 깜빡거리는 총알을 여러 개의 일직선으로 6개 발사한다.
    private void Pattern_3()
    {

        isPatternStart = true;


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

        if (curPatternCount > 1)
        {
            isPatternStart = false;
        }
    }
    #endregion

    #region Pattern_4
    //  [YHJ] 2023-03-16
    //  @brief 빙글빙글 돌면서 방 전체에 총알을 일정하게 연사한 뒤, 마지막으로 약간 더 빠른 총알을 자신을 중심으로 원형으로 발사한다.
    private void Pattern_4()
    {

        isPatternStart = true;
        bulletkingAnimator.SetTrigger("isPattern_3");

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
                maxPatternCount = 1;
                bulletSpeed = 5f;
                bulletCount = 50;

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
        else
        {
            Invoke("Pattern", 2f);
        }

        if (curPatternCount > 1)
        {
            isPatternStart = false;
        }
    }
    #endregion

    #region Pattern_5
    //  [YHJ] 2023-03-16
    //  @brief 자신의 위쪽에 커다란 총알을 한 발 발사한다. 발사된 총알은 여러 개의 깜빡거리는 탄환 여러 개로 나뉘고 이 탄환들은 잠시 뒤 여러 갈래로 퍼져나간다.
    IEnumerator Pattern_5()
    {

        isPatternStart = true;

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
        else
        {
            Invoke("Pattern", 2f);
        }

        if (curPatternCount > 1)
        {
            isPatternStart = false;
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
    #endregion

    #region Pattern_6
    //  [YHJ] 2023-03-16
    //  @brief 플레이어를 향해 화염병처럼 화염 장판을 원형으로 까는 술잔을 던진다.
    private void Pattern_6()
    {

        //isPatternStart = true;

        //maxPatternCount = 1;
        //bulletSpeed = 3f;
        //bulletCount = 1;

        //for (int i = 0; i < bulletCount; i++)
        //{
        //    GameObject bullet = objectManager.MakeObject("Bullet_TypeD");
        //    bullet.transform.position = transform.position;
        //    bullet.transform.rotation = Quaternion.identity;

        //    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        //    Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / bulletCount), Mathf.Sin(Mathf.PI * 2 * i / bulletCount));
        //    bulletRigidbody.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        //}

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_6", 0.7f);
        }
        else
        {
            Invoke("Pattern", 2f);
        }

        if (curPatternCount > 1)
        {
            isPatternStart = false;
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            StartCoroutine("OnHit");
        }
    }

    IEnumerator OnHit()
    {
        yield return new WaitForSeconds(0.1f);
        throneSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        throneSpriteRenderer.color = Color.white;
    }
}