using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyWeapon : MonoBehaviour
{
    float angle;
    GameObject player;
    public RaycastHit2D hit = default;

    public float delayTime;

    bool isDelayEnd;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // { Look At Player Position
        angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x)
        * Mathf.Rad2Deg;

        if (transform.parent.localPosition.x <= 0)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        else if (transform.parent.localPosition.x > 0)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.back);

            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        }
        // } Look At Player Position
    }
    public void fire()
    {
        if (isDelayEnd == false)
        {
            isDelayEnd = true;
            Debug.Log("fire bullet");
            GameObject clone = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
            clone.transform.SetParent(GameObject.Find("GameObjs").transform);
            clone.GetComponent<TestBullet>().bulletType = 0;
            StartCoroutine(FireDelay(delayTime));
        }
    }
    IEnumerator FireDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        isDelayEnd = false;
    }
}
