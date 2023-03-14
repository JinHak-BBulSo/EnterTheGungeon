using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    GameObject hand;
    TestEnemyWeapon weapon;
    float dist;
    bool isDelayEnd;

    float angle;
    GameObject player;


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
        hand = transform.GetChild(1).gameObject;
        weapon = hand.transform.GetChild(0).GetComponent<TestEnemyWeapon>();

        player = GameObject.FindWithTag("Player");

        StartCoroutine(SpawnTime());
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
            if (weapon.hit != default)
            {
                if (weapon.hit.transform.tag == "Player")
                {
                    weapon.GetComponent<TestEnemyWeapon>().fire();
                }
                else if (weapon.GetComponent<TestEnemyWeapon>().hit.collider.tag != "Player")
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
        Debug.Log("Move to player");
        isMove = true;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition, 0.3f);
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

    IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(1.5f);
        IsSpawnEnd = true;
        transform.GetChild(0).GetComponent<Animator>().SetBool("IsSpawnEnd", true);
    }
}
