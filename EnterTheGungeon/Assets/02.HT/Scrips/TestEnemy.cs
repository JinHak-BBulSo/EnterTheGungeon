using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    GameObject hand;
    GameObject weapon;
    float dist;
    bool isDelayEnd;

    float angle;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        hand = transform.GetChild(1).gameObject;
        weapon = hand.transform.GetChild(0).gameObject;

        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition);
        if (dist > 500)
        {
            Move();
        }
        else { }

        // { Raycast To Player & Condition Check RayCast Hit
        if (weapon.GetComponent<TestEnemyWeapon>().hit.collider.tag == "Player")
        {
            if (isDelayEnd == false)
            {
                isDelayEnd = true;
                weapon.GetComponent<TestEnemyWeapon>().fire();
                StartCoroutine(FireDelay());
            }
        }

        else if (weapon.GetComponent<TestEnemyWeapon>().hit.collider.tag != "Player")

        {
            Move();
        }
        // } Raycast To Player & Condition Check RayCast Hit 

        // { Calc Player Angle from this Position
        angle = Quaternion.FromToRotation(Vector3.up, (player.transform.position - transform.position)).eulerAngles.z;
        if (angle >= 90 && angle < 180)
        {
            //idle left
        }
        else if (angle >= 180 && angle < 270)
        {
            //idle right
        }
        else
        {
            //idle back
        }
        // } Calc Player Angle from this Position
    }
    void Move()
    {
        Debug.Log("Move to player");
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition, 0.3f);
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(2);
        isDelayEnd = false;
    }
}
