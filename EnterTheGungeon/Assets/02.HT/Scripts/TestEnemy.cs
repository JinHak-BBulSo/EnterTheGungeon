using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestEnemy : MonoBehaviour
{
    public int attackType;

    TestEnemyWeapon weapon;
    TestEnemyEye eye;
    GameObject hand;
    float dist;
    bool isDelayEnd;

    float angle;
    GameObject player;

    Vector2 direction;

    public float moveSpeed;

    // { animation var

    bool isTargetLeft;
    bool isTargetRight;
    bool isTargetLeftBack;
    bool isTargetRightBack;

    bool isMove;
    bool IsSpawnEnd;
    bool isAttack;

    // } animation var


    // { Var for summonBullet
    float xValue;
    float yValue;
    Vector2 directionForSummonBullet;
    Vector2 summonBulletPosition;
    float angleForSummonBullet;
    // } Var for summonBullet

    //If FSM R&D, Rebuild animaion



    // { Var using for FindPlayerDirection
    float minDist = default;
    float getDist;
    float minDistArrayIndex;

    Vector2 dir1;
    Vector2 dir2;
    Vector2 dir3;
    Vector2 dir4;
    Vector2 dir5;
    Vector2 dir6;
    Vector2 dir7;
    Vector2 dir8;

    Vector2[] directionArray = new Vector2[8];
    // } Var using for FindPlayerDirection

    // Start is called before the first frame update
    void Start()
    {
        eye = transform.GetChild(1).gameObject.GetComponent<TestEnemyEye>();

        if (attackType == 0)
        {
            hand = transform.GetChild(2).gameObject;
            weapon = hand.transform.GetChild(0).GetComponent<TestEnemyWeapon>();
        }

        player = GameObject.FindWithTag("Player");

        StartCoroutine(SpawnTime());
        moveSpeed = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsSpawnEnd) { }
        else
        {

            isMove = false;
            isAttack = transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("attack");

            dist = Vector2.Distance(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition);
            if (dist > 500 && !isAttack)
            {
                Move();
            }
            else { }

            // { Raycast To Player & Condition Check RayCast Hit
            if (eye.hit != default)
            {
                if (eye.hit.collider.tag == "Player")
                {
                    Attack();
                }
                else if (eye.hit.collider.tag != "Player" && !isAttack)
                {
                    Move();
                }
            }
            // } Raycast To Player & Condition Check RayCast Hit


            // { set var for using animation
            direction = player.transform.position - transform.position;
            transform.GetChild(0).GetComponent<Animator>().SetFloat("inputX", direction.x);
            transform.GetChild(0).GetComponent<Animator>().SetFloat("inputY", direction.y);
            // ] set var for using animation
        }

        if (isMove)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", false);
        }

        FindPlayerDirection();
    }
    void Move()
    {
        isMove = true;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition, moveSpeed);
    }
    void Attack()
    {
        // use weapon
        if (attackType == 0)
        {
            weapon.fire();
        }
        // summon bullet
        else if (attackType == 1)
        {
            if (!isDelayEnd)
            {
                isDelayEnd = true;
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("IsAttack");
                StartCoroutine(Fire(1, 5));
            }
        }
        else
        {

        }
    }
    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(1.5f);
        IsSpawnEnd = true;
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsSpawnEnd", true);
    }

    IEnumerator Fire(float castTime_, float delayTime_)   //castime & delaytime < scriptableobj에 값 추가 예정
    {
        yield return new WaitForSeconds(castTime_);
        // { enemy pattern
        if (this.name == "bookllet")
        {
            float[] xPos = { -30, -30, -30, -30, -30, -30, -30, -15, -8.35f, 15, 30, 35, 30, 15, -8.35f, -15, 15, 25, 35 };
            float[] yPos = { -45, -30, -15, 0, 15, 30, 45, 45, 45, 45, 40, 20, 5, 0, 0, 0, -15, -30, -45 };
            for (int i = 0; i < 19; i++)
            {
                GameObject clone = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
                clone.GetComponent<TestBullet>().bulletType = 1;
                clone.transform.SetParent(GameObject.Find("GameObjs").transform);
                clone.transform.localPosition = new Vector2(transform.localPosition.x + xPos[i], transform.localPosition.y + yPos[i]);
            }
        }

        if (this.name == "gunNut")
        {
            for (float i = minDistArrayIndex * 45; i <= minDistArrayIndex * 45 + 90; i += 10)
            {
                int j = (int)minDistArrayIndex;
                Debug.Log(directionArray[j]);


                GameObject clone = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
                clone.GetComponent<TestBullet>().bulletType = 1;
                clone.transform.SetParent(GameObject.Find("GameObjs").transform);

                angleForSummonBullet = i * Mathf.PI / 180.0f;
                xValue = Mathf.Cos(angleForSummonBullet);
                yValue = Mathf.Sin(angleForSummonBullet);
                directionForSummonBullet = new Vector2(xValue, yValue);
                summonBulletPosition = (Vector2)transform.localPosition + directionForSummonBullet * 150;
                clone.transform.localPosition = summonBulletPosition;
            }
        }
        // } enemy pattern
        yield return new WaitForSeconds(delayTime_);
        isDelayEnd = false;
    }

    // @brief find player's direction from current enemy position.
    void FindPlayerDirection()
    {
        dir1 = (Vector2.up + Vector2.right).normalized;
        dir2 = (Vector2.up).normalized;
        dir3 = (Vector2.left + Vector2.up).normalized;
        dir4 = Vector2.left.normalized;
        dir5 = (Vector2.down + Vector2.left).normalized;
        dir6 = (Vector2.down).normalized;
        dir7 = (Vector2.right + Vector2.down).normalized;
        dir8 = (Vector2.right).normalized;


        Vector2[] directionArray_ = { dir1, dir2, dir3, dir4, dir5, dir6, dir7, dir8 };

        directionArray = directionArray_;

        for (int i = 0; i < directionArray_.Length; i++)
        {
            getDist = Vector2.Distance((player.transform.position - transform.position).normalized, directionArray_[i]);
            if (i == 0 || minDist > getDist)
            {
                minDist = getDist;
            }
            else { }

            if (minDist == getDist)
            {
                minDistArrayIndex = i;
            }
            else { }
        }
    }
}

