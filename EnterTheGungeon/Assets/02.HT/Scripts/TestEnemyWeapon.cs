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

    ObjectPool objectPool;
    List<GameObject> enemyBulletPool;
    GameObject enemyBulletPrefab;
    TestEnemy testEnemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        testEnemy = transform.parent.parent.GetComponent<TestEnemy>();
        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        enemyBulletPool = objectPool.enemyBulletPool;
        enemyBulletPrefab = objectPool.enemyBulletPrefab;
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
            GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
            clone_.transform.position = transform.position;
            //GameObject clone = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
            //clone.transform.SetParent(GameObject.Find("GameObjs").transform);
            clone_.GetComponent<TestBullet>().bulletType = 0;
            clone_.GetComponent<TestBullet>().enemyName = testEnemy.enemyName;
            clone_.GetComponent<Rigidbody2D>().velocity = transform.up * 5;
            StartCoroutine(FireDelay(delayTime));
        }
    }
    IEnumerator FireDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        isDelayEnd = false;
    }
}
