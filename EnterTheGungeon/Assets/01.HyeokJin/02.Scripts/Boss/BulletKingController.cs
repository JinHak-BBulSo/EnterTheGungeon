using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletKingController : Boss
{
    private Animator bulletkingAnimator = default;
    private GameObject player = default;
    private GameObject bossBulletKing = default;
    private ObjectManager objectManager = default;
    private SpriteRenderer throneSpriteRenderer = default;
    public GameObject vfxPrefab = default;
    private Animator vfxAnimator = default;

    private GameObject bossHpBar = default;
    private TMP_Text bossName = default;
    private Image innerHpBar = default;

    private int maxHp = default;
    private float decreaseAmount = default;

    private GameObject muzzle = default;
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
    private GameObject muzzle_Hand = default;

    private float bulletCount = default;
    private float bulletSpeed = default;
    private float moveSpeed = default;
    private float enemyRadius = default;

    private int patternIndex = default;
    private int curPatternCount = default;
    private int maxPatternCount = default;
    private int bulletGap = default;

    private bool isDead = false;

    private int vfxIndex = default;
    private float vfxRangeX = default;
    private float vfxRangeY = default;


    private bool isExpolded = false;

    private bool isPatterenCountReset = false;
    private bool isMoving = false;
    private bool isPatternStart = false;


    private float distance = default;

    private float direction_X = default;
    private float direction_Y = default;


    private void OnEnable()
    {
        maxHp = 50;
        currentHp = maxHp;
        Invoke("Status", 4f);
        bossHpBar.SetActive(true);
    }

    private void OnDisable()
    {
        bossHpBar.SetActive(false);
        CancelInvoke();
        StopAllCoroutines();
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        //player = PlayerManager.Instance.player.gameObject;
        bossBulletKing = transform.parent.gameObject;

        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        bulletkingAnimator = gameObject.transform.parent.GetComponent<Animator>();

        throneSpriteRenderer = GetComponent<SpriteRenderer>();

        muzzle = bossBulletKing.transform.GetChild(3).gameObject;

        muzzle_Left_1 = muzzle.transform.GetChild(0).gameObject;
        muzzle_Left_2 = muzzle.transform.GetChild(1).gameObject;
        muzzle_Left_3 = muzzle.transform.GetChild(2).gameObject;
        muzzle_Left_4 = muzzle.transform.GetChild(3).gameObject;
        muzzle_Left_5 = muzzle.transform.GetChild(4).gameObject;
        muzzle_Right_1 = muzzle.transform.GetChild(5).gameObject;
        muzzle_Right_2 = muzzle.transform.GetChild(6).gameObject;
        muzzle_Right_3 = muzzle.transform.GetChild(7).gameObject;
        muzzle_Right_4 = muzzle.transform.GetChild(8).gameObject;
        muzzle_Right_5 = muzzle.transform.GetChild(9).gameObject;
        muzzle_Hand = muzzle.transform.GetChild(10).gameObject;

        bossHpBar = GameObject.Find("BossHpBar").gameObject;
        bossName = bossHpBar.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        innerHpBar = bossHpBar.transform.GetChild(1).gameObject.GetComponent<Image>();
        bossName.text = "Bllet King";

        //vfxPrefab = transform.GetChild(0).gameObject;
        vfxAnimator = vfxPrefab.GetComponent<Animator>();
        bossHpBar.SetActive(false);
    }

    private void Update()
    {
        Move();
        CheckDie();
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

        if (!isDead)
        {
            moveSpeed = 1.5f;

            distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance > 8)
            {
                isMoving = true;
                Vector3 direction = (player.transform.position - transform.position).normalized;
                bossBulletKing.transform.position += direction * moveSpeed * Time.deltaTime;
            }
            else
            {
                isMoving = false;
            }
        }

    }   //  Move()
    #endregion

    private void CheckDie()
    {
        if (currentHp <= 0)
        {
            isDead = true;

            // { [HT] add
            PlayerManager.Instance.location = "Gungeon Proper";
            // } [HT] add
        }
    }

    #region Status
    private void Status()
    {
        if (!isDead)
        {
            patternIndex = Random.Range(1, 7);

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
                    Pattern_5();
                    break;
                case 6:
                    Pattern_6();
                    break;
                case 7:
                    Die();
                    break;
            }
        }
        else
        {
            Die();
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

    #region Pattern_1_3way
    IEnumerator Pattern_1_3way()
    {
        isPatternStart = true;
        bulletkingAnimator.SetBool("isPattern_1", true);

        if (curPatternCount == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }

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


        if (!isDead && curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_1", 0.05f);
        }
        else
        {
            bulletkingAnimator.SetBool("isPattern_1", false);
            Invoke("Status", 2f);
        }
    }   //  Pattern_1_3way()
    #endregion
    #endregion

    #region Pattern_2
    private void Pattern_2()
    {
        StartCoroutine("Pattern_2_All");
    }   //  Pattern_2()

    #region Pattern_2_All
    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 나아가는 총알을 4겹 원형으로 발사한다.
    IEnumerator Pattern_2_All()
    {
        bulletkingAnimator.SetBool("isPattern_2", true);

        if (curPatternCount == 0)
        {
            yield return new WaitForSeconds(0.7f);
        }

        isPatternStart = true;

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


        if (!isDead && curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_2", 0.5f);
        }
        else
        {
            bulletkingAnimator.SetBool("isPattern_2", false);
            Invoke("Status", 2f);
        }
    }   //  Pattern_2_All()
    #endregion
    #endregion

    #region Pattern_3
    private void Pattern_3()
    {
        StartCoroutine("Pattern_3_All");
    }   //  Pattern_3()

    #region Pattern_3_All
    //  [YHJ] 2023-03-16
    //  @brief 자신을 중심으로 퍼져 나가는 깜빡거리는 총알을 여러 개의 일직선으로 6개 발사한다.
    IEnumerator Pattern_3_All()
    {
        bulletkingAnimator.SetBool("isPattern_3", true);

        if (curPatternCount == 0)
        {
            yield return new WaitForSeconds(1f);
        }

        isPatternStart = true;

        maxPatternCount = 6;
        bulletSpeed = 6f;
        bulletCount = 40;
        bulletGap = 4;
        enemyRadius = 1.8f;

        for (int i = 0; i < bulletCount; i++)
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

        if (!isDead && curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_3", 0.1f);
        }
        else
        {
            bulletkingAnimator.SetBool("isPattern_3", false);
            Invoke("Status", 2f);
        }
    }   //  Pattern_3_All()
    #endregion
    #endregion

    #region Pattern_4
    //  [YHJ] 2023-03-16
    //  @brief 빙글빙글 돌면서 방 전체에 총알을 일정하게 연사한 뒤, 마지막으로 약간 더 빠른 총알을 자신을 중심으로 원형으로 발사한다.
    private void Pattern_4()
    {
        bulletkingAnimator.SetBool("isPattern_4", true);

        isPatternStart = true;

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

        if (!isDead && curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_4", 0.2f);
        }
        else
        {
            bulletkingAnimator.SetBool("isPattern_4", false);
            Invoke("Status", 2f);
        }
    }
    #endregion

    #region Pattern_5
    //  [YHJ] 2023-03-16
    //  @brief 자신의 위쪽에 커다란 총알을 한 발 발사한다. 발사된 총알은 여러 개의 깜빡거리는 탄환 여러 개로 나뉘고 이 탄환들은 잠시 뒤 여러 갈래로 퍼져나간다.
    private void Pattern_5()
    {
        StartCoroutine("Pattern_5_All");
    }

    #region Pattern_5_All
    IEnumerator Pattern_5_All()
    {
        if (curPatternCount == 0)
        {
            bulletkingAnimator.SetBool("isPattern_5", true);
            yield return new WaitForSeconds(1.5f);
        }

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

        if (!isDead && curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_5_All", 0.7f);
        }
        else
        {
            bulletkingAnimator.SetBool("isPattern_5", false);
            Invoke("Status", 2f);
        }
    }   //  Pattern_5_All()
    #endregion

    #region Pattern_5_ExplodeBullet_1
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
    }   //  Pattern_5_ExplodeBullet_1()
    #endregion

    #region Pattern_5_ExplodeBullet_2
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
    }   //  Pattern_5_ExplodeBullet_2()
    #endregion

    #region Pattern_5_ExplodeBullet_3
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
    }   //  Pattern_5_ExplodeBullet_3()
    #endregion
    #endregion

    #region Pattern_6
    private void Pattern_6()
    {
        StartCoroutine("Pattern_6_All");
    }   //  Pattern_6()

    #region Pattern_6_All
    //  [YHJ] 2023-03-16
    //  @brief 플레이어를 향해 화염병처럼 화염 장판을 원형으로 까는 술잔을 던진다.
    IEnumerator Pattern_6_All()
    {
        isPatternStart = true;

        if (curPatternCount == 0)
        {
            bulletkingAnimator.SetBool("isPattern_6", true);
            yield return new WaitForSeconds(1f);
        }


        maxPatternCount = 1;
        bulletSpeed = 8f;
        bulletCount = 1;

        Vector2 playerPosition = player.transform.position;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = objectManager.MakeObject("Bullet_TypeD");
            bullet.transform.position = muzzle_Hand.transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            Vector2 direction = new Vector2(playerPosition.x - bullet.transform.position.x, playerPosition.y - bullet.transform.position.y);
            bulletRigidbody.velocity = direction.normalized * bulletSpeed;
            bulletRigidbody.angularVelocity = 500f;

            StartCoroutine(Pattern_6_StopBullet(bullet, playerPosition, 0.5f));
        }

        curPatternCount++;

        if (!isDead && curPatternCount < maxPatternCount)
        {
            Invoke("Pattern_6", 0.7f);
        }
        else
        {
            bulletkingAnimator.SetBool("isPattern_6", false);
            Invoke("Status", 2f);
        }
    }   //  Pattern_6_All()
    #endregion

    #region Pattern_6_StopBullet
    IEnumerator Pattern_6_StopBullet(GameObject bullet_, Vector2 targetPosition_, float distanceThreshold_)
    {
        while (true)
        {
            float distance = Vector2.Distance(bullet_.transform.position, targetPosition_);

            if (distance < distanceThreshold_)
            {
                bullet_.GetComponent<Animator>().SetTrigger("isExplode");
                bullet_.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                bullet_.GetComponent<Rigidbody2D>().angularVelocity = 0f;

                yield return new WaitForSeconds(1f);
                bullet_.GetComponent<CircleCollider2D>().enabled = false;

                break;
            }
            yield return null;
        }
    }   //  Pattern_6_StopBullet()
    #endregion
    #endregion

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet") && !isDead)
        {
            int playerBulletDamage_ = collision.gameObject.GetComponentMust<PlayerBullet>().bulletDamage;
            StartCoroutine(OnHit_HpBar(playerBulletDamage_));
            StartCoroutine("OnHit_Color");
        }
    }

    IEnumerator OnHit_HpBar(int playerBulletDamage_)
    {
        // int bulletDMG = 120;

        if (currentHp > 0)
        {
            decreaseAmount = 0.001f;

            currentHp -= playerBulletDamage_;

            while (innerHpBar.fillAmount > (float)currentHp / (float)maxHp)
            {
                innerHpBar.fillAmount -= decreaseAmount;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            isDead = true;
        }

    }   //  OnHit_HpBar()

    IEnumerator OnHit_Color()
    {
        yield return new WaitForSeconds(0.1f);
        throneSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        throneSpriteRenderer.color = Color.white;
    }   //  OnHit_Color()

    private void Die()
    {
        Debug.Log("뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐뒤짐");

        bulletkingAnimator.SetTrigger("isDead");
        StartCoroutine("Die_VFX");
    }

    IEnumerator Die_VFX()
    {
        vfxIndex = 10;

        float randomTime = Random.Range(0.08f, 0.55f);

        for (int i = 0; i < vfxIndex; i++)
        {
            vfxRangeX = Random.Range(-1.5f, 1.5f);
            vfxRangeY = Random.Range(-1.8f, 1.8f);

            Vector3 createPosition = bossBulletKing.transform.position + new Vector3(vfxRangeX, vfxRangeY, 0f);
            GameObject vfx = Instantiate(vfxPrefab, createPosition, Quaternion.identity);
            vfx.SetActive(true);

            yield return new WaitForSeconds(randomTime);
        }
        yield return new WaitForSeconds(3f);
    }
}
