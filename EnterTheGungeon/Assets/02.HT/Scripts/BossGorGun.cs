using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGorGun : MonoBehaviour
{
    TestEnemyEye eye;
    GameObject player;
    Quaternion defaultRotation;

    // { Var for move
    float moveSpeed;
    public float defaultMoveSpeed;
    public bool isMovepattern;
    bool ispattern1StartEnd;
    bool durationTimeCheck;

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

    void Start()
    {
        eye = transform.GetChild(1).gameObject.GetComponent<TestEnemyEye>();
        player = GameObject.FindWithTag("Player");
        defaultMoveSpeed = 1f;
        moveSpeed = defaultMoveSpeed;
        defaultRotation = transform.rotation;

        rigid = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<CapsuleCollider2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        bodyImage = transform.GetChild(0).GetComponent<Image>();
        bodyImageRectTransform = transform.GetChild(0).GetComponent<RectTransform>();

        effectObject = transform.GetChild(0).GetChild(0).gameObject;
        effectImage = effectObject.GetComponent<Image>();
        effectImageRectTransform = effectObject.GetComponent<RectTransform>();
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

        Move();

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
            if (distance > 300)
            {
                moveSpeed = defaultMoveSpeed;
                transform.rotation = defaultRotation;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, player.transform.localPosition, moveSpeed);
            }
            else { }
        }
    }

    void TargetingPlayer()
    {
        angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x)
        * Mathf.Rad2Deg;
        Quaternion lookRotation_ = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation_, 1f * Time.deltaTime);
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


    }
}



//이동 패턴 1//이동패턴1 완료 후 공격 3~4번진행 공격 패턴 간 기본이동(move toward)//반복//
