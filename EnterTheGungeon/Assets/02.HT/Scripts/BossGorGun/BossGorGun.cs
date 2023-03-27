using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGorGun : MonoBehaviour
{
    public string enemyName;
    
    TestEnemyEye eye;
    GameObject player;
    Quaternion defaultRotation;

    // { Var for move
    float moveSpeed;
    public float defaultMoveSpeed;
    public bool isMovepattern;
    bool ispattern1StartEnd;
    bool durationTimeCheck;

    bool isFirstTargeting;

    Rigidbody2D rigid;
    CapsuleCollider2D bossCollider;
    float angle;

    float distance;

    // } Var for move


    // { Var for Image
    Animator anim;
    Image bodyImage;
    RectTransform bodyImageRectTransform;

    GameObject effectObject;
    Image effectImage;
    RectTransform effectImageRectTransform;
    // } Var for Image

    // { Var for Attack
    public bool isAttackPattern;

    BossGorgunBody bossGorgunBody;
    // } Var for Attack

    // Var for AttackPattern1
    public bool isPlayerUpside;

    int maxHp;
    int currentHp;
    public bool isDead;

    //피격시 받는 데미지 체크를 위한 변수
    int damageTaken;
    public bool isChangeTostatue;



    ObjectPool objectPool;
    List<GameObject> poisonAreaPool;
    GameObject poisonAreaPrefab;

    void Start()
    {
        enemyName = "Gorgun";

        maxHp = 975;
        currentHp = 975;

        eye = transform.GetChild(1).gameObject.GetComponent<TestEnemyEye>();
        player = GameObject.FindWithTag("Player");
        defaultMoveSpeed = 1f;
        moveSpeed = defaultMoveSpeed;
        defaultRotation = transform.rotation;

        rigid = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<CapsuleCollider2D>();
        anim = transform.GetChild(0).GetComponent<Animator>(); ;
        bodyImage = transform.GetChild(0).GetComponent<Image>();
        bodyImageRectTransform = transform.GetChild(0).GetComponent<RectTransform>();

        effectObject = transform.GetChild(0).GetChild(0).gameObject;
        effectImage = effectObject.GetComponent<Image>();
        effectImageRectTransform = effectObject.GetComponent<RectTransform>();

        bossGorgunBody = transform.GetChild(0).GetComponent<BossGorgunBody>();

        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        poisonAreaPool = objectPool.poisonAreaPool;
        poisonAreaPrefab = objectPool.poisonAreaPrefab;
    }

    void Update()
    {
        distance = Vector2.Distance(player.transform.localPosition, transform.localPosition);

        if (!isMovepattern)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            bossCollider.isTrigger = false;
            anim.SetBool("isMovePattern1", false);
        }

        if (!isDead)
        {
            Move();
            attack();
            EndAttack();
        }

        if (currentHp <= 0)
        {
            isDead = true;
        }
        if (isDead)
        {

            Die();
        }
        ImageSizeSet();
    }

    void ImageSizeSet()
    {
        bodyImage.SetNativeSize();
        bodyImageRectTransform.sizeDelta = new Vector2(bodyImageRectTransform.sizeDelta.x * 3, bodyImageRectTransform.sizeDelta.y * 3);
    }

    void Move()
    {
        if (isMovepattern)
        {
            bossCollider.isTrigger = true;

            if (ispattern1StartEnd == false)
            {
                anim.SetTrigger("isMovePattern1Start");
                effectObject.SetActive(true);
                effectImage.SetNativeSize();
                effectImageRectTransform.sizeDelta = new Vector2(effectImageRectTransform.sizeDelta.x * 3, effectImageRectTransform.sizeDelta.y * 3);

                ispattern1StartEnd = true;
                isFirstTargeting = true;
            }
            else { }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("movePatten1"))
            {
                effectObject.SetActive(false);
                rigid.constraints = RigidbodyConstraints2D.None;
                anim.SetBool("isMovePattern1", true);
                moveSpeed = defaultMoveSpeed * 5;
                rigid.velocity = transform.up * moveSpeed;
                TargetingPlayer();

                MakePoisonArea();
            }
            else { }

            //일정 시간 지나거나, 벽에 부딛혔을때 패턴 종료
            if (!durationTimeCheck)
            {
                StartCoroutine(MovePatternDurationTime(5f));
            }

        }
        if (!isMovepattern)
        {
            //조건 추가 공격중이 아닐때
            if (distance > 300)
            {
                if (!isAttackPattern)
                {
                    moveSpeed = defaultMoveSpeed;
                    transform.rotation = defaultRotation;
                    Vector2 dir_ = player.transform.position - transform.position;
                    rigid.velocity = dir_.normalized * moveSpeed;
                }

                //transform.localPosition = Vector3.MoveTowards(transform.localPosition, player.transform.localPosition, moveSpeed);
            }
            else
            {
                Vector2 dir_ = player.transform.position - transform.position;
                rigid.velocity = Vector2.zero;
            }
        }
    }

    void TargetingPlayer()
    {
        angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x)
        * Mathf.Rad2Deg;
        Quaternion lookRotation_ = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        if (isFirstTargeting)
        {
            transform.rotation = lookRotation_;
            isFirstTargeting = false;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation_, 1f * Time.deltaTime);
    }

    void MakePoisonArea()
    {
        GameObject poisonArea_ = objectPool.GetObject(poisonAreaPool, poisonAreaPrefab, 2);
        poisonArea_.transform.position = transform.position;
        poisonArea_.GetComponent<PoisonArea>().enemyName = enemyName;
        poisonArea_.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            anim.SetBool("isMovePattern1", false);
            isMovepattern = false;
            ispattern1StartEnd = false;
            rigid.velocity = Vector2.zero;
            transform.rotation = defaultRotation;
            //
            isAttackPattern = true;
            pattrenNum = Random.Range(0, 4);
        }

        // @brief colide player bullet, take damage and apply currentHp.
        if (other.tag == "PlayerBullet")
        {
            damageTaken = 0;// = other.GetComponent<PlayerBullet>().damage; after setting playerbullet, change this.
            currentHp -= damageTaken;
            damageTaken = 0;

            if (isChangeTostatue)
            {
                Destroy(this.gameObject);
            }
        }
    }
    IEnumerator MovePatternDurationTime(float durationTime_)
    {
        durationTimeCheck = true;
        yield return new WaitForSeconds(durationTime_);
        anim.SetBool("isMovePattern1", false);
        isMovepattern = false;
        ispattern1StartEnd = false;
        rigid.velocity = Vector2.zero;
        transform.rotation = defaultRotation;
        durationTimeCheck = false;
        //
        yield return new WaitForSeconds(1);
        isAttackPattern = true;
        pattrenNum = Random.Range(0, 4);

    }
    int attackPatternCount;
    int pattrenNum;
    void attack()
    {
        if (isAttackPattern)
        {
            anim.SetBool("isAttack", true);
            //attack pattern1
            if (pattrenNum == 0)
            {
                anim.SetFloat("attackPattern", 0);
                if (bossGorgunBody.patternCount == 5)
                {
                    anim.SetBool("isAttack", false);
                    attackPatternCount++;

                    //
                    isAttackPattern = false;
                    //isMovepattern = true;
                    bossGorgunBody.patternCount = 0;
                    if (!AttackDelayCheck)
                    {
                        StartCoroutine(AttackDelay());
                    }

                }
            }
            if (pattrenNum == 1)
            {
                // @brief when player position is upside of gorgun/downside of gorgun
                if (player.transform.position.y > transform.position.y)
                {
                    isPlayerUpside = true;
                    anim.SetFloat("attackPattern", 1 / 4f);
                }
                if (player.transform.position.y < transform.position.y)
                {
                    isPlayerUpside = false;
                    anim.SetFloat("attackPattern", 2 / 4f);
                }

                if (bossGorgunBody.patternCount == 2)
                {
                    anim.SetBool("isAttack", false);

                    attackPatternCount++;
                    //
                    isAttackPattern = false;
                    //isMovepattern = true;
                    bossGorgunBody.patternCount = 0;
                    if (!AttackDelayCheck)
                    {
                        StartCoroutine(AttackDelay());
                    }
                }
            }
            if (pattrenNum == 2)
            {
                anim.SetFloat("attackPattern", 3 / 4f);
                if (bossGorgunBody.patternCount == 1)
                {
                    anim.SetBool("isAttack", false);
                    attackPatternCount++;
                    isAttackPattern = false;
                    bossGorgunBody.patternCount = 0;
                    if (!AttackDelayCheck)
                    {
                        StartCoroutine(AttackDelay());
                    }

                }
            }

            if (pattrenNum == 3)
            {
                anim.SetFloat("attackPattern", 1);
                if (bossGorgunBody.patternCount == 1)
                {
                    anim.SetBool("isAttack", false);
                    attackPatternCount++;
                    isAttackPattern = false;
                    bossGorgunBody.patternCount = 0;
                    if (!AttackDelayCheck)
                    {
                        StartCoroutine(AttackDelay());
                    }

                }
            }
        }
    }

    bool AttackDelayCheck;
    IEnumerator AttackDelay()
    {
        if (attackPatternCount != 3)
        {
            AttackDelayCheck = true;
            yield return new WaitForSeconds(1);
            isAttackPattern = true;
            pattrenNum = Random.Range(0, 4);
            AttackDelayCheck = false;
        }

    }

    void EndAttack()
    {
        if (attackPatternCount == 3)
        {
            isMovepattern = true;
            attackPatternCount = 0;
        }
    }

    void Die()
    {
        if (isDead == true)
        {
            isDead = false;
            if (effectObject.activeSelf == true)
            {
                effectObject.SetActive(false);
            }
            anim.SetTrigger("isDead");

            // exception: while movepattern, take dot damage to die
            rigid.constraints = RigidbodyConstraints2D.FreezePositionY;
            bossCollider.isTrigger = false;
            // exception: while movepattern, take dot damage to die

            StopAllCoroutines();
        }
    }


}