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

    public float moveSpeed;

    // { animation var

    bool isTargetLeft;
    bool isTargetRight;
    bool isTargetLeftBack;
    bool isTargetRightBack;

    bool isMove;
    bool IsSpawnEnd;

    // } animation var

    //If FSM R&D, Rebuild animaion

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

            dist = Vector2.Distance(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition);
            if (dist > 500)
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
                else if (eye.hit.collider.tag != "Player")
                {
                    Move();
                }
            }
            // } Raycast To Player & Condition Check RayCast Hit

            // { Calc Player Angle from this Position & animation apply
            CheckAngle();
            AnimationSet();
            // } Calc Player Angle from this Position & animation apply
        }


    }
    void Move()
    {
        isMove = true;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition, moveSpeed);
    }

    void CheckAngle()
    {
        isTargetLeft = false;
        isTargetRight = false;
        isTargetLeftBack = false;
        isTargetRightBack = false;

        angle = Quaternion.FromToRotation(Vector3.up, (player.transform.position - transform.position)).eulerAngles.z;
        if (angle >= 90 && angle < 180)
        {
            isTargetLeft = true;
        }
        else if (angle >= 180 && angle < 270)
        {
            isTargetRight = true;
        }
        else if (angle >= 0 && angle < 90)
        {
            isTargetLeftBack = true;
        }
        else if (angle >= 270 && angle < 360)
        {
            isTargetRightBack = true;
        }

    }

    void AnimationSet()
    {
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookLeft", false);
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookRight", false);
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookLeftBack", false);
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookRightBack", false);

        if (isTargetLeft)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookLeft", true);
        }
        else if (isTargetRight)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookRight", true);
        }
        else if (isTargetLeftBack)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookLeftBack", true);
        }
        else if (isTargetRightBack)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsLookRightBack", true);
        }
        else { }


        if (isMove == true)
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", true);
        }
        else
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("IsMove", false);
        }
    }


    //scriptableObject 생성 후 변경
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
            if (this.name == "bookllet")
            {
                float[] xPos = { -30, -30, -30, -30, -30, -30, -30, -15, -8.35f, 15, 30, 35, 30, 15, -8.35f, -15, 15, 25, 35 };
                float[] yPos = { -45, -30, -15, 0, 15, 30, 45, 45, 45, 45, 40, 20, 5, 0, 0, 0, -15, -30, -45 };

                if (!isDelayEnd)
                {
                    isDelayEnd = true;
                    for (int i = 0; i < 19; i++)
                    {
                        GameObject clone = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
                        clone.GetComponent<TestBullet>().bulletType = 1;
                        clone.transform.SetParent(GameObject.Find("GameObjs").transform);
                        clone.transform.localPosition = new Vector2(transform.localPosition.x + xPos[i], transform.localPosition.y + yPos[i]);
                    }
                    transform.GetChild(0).GetComponent<Animator>().SetBool("IsAttack", true);//trigger로 교체?
                    

                    StartCoroutine(FireDelay(5));
                }

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

    IEnumerator FireDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsAttack", false);

        isDelayEnd = false;
    }
}

