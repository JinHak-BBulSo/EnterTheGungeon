using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    GameObject hand;
    GameObject weapon;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        hand = transform.GetChild(1).gameObject;
        weapon = hand.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition);
        if (dist > 250)
        {
            //Move();
        }
        else { }

        // { Raycast To Player & Condition Check RayCast Hit
        if (weapon.GetComponent<TestEnemyWeapon>().hit.collider.tag == "Player")
        {
            weapon.GetComponent<TestEnemyWeapon>().fire();
        }

        else if (weapon.GetComponent<TestEnemyWeapon>().hit.collider.tag != "Player")

        {
            //Move();
        }
        // } Raycast To Player & Condition Check RayCast Hit 
    }
    void Move()
    {
        Debug.Log("Move to player");
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, GameObject.Find("TestPlayer").transform.localPosition, 1f);
    }
}
