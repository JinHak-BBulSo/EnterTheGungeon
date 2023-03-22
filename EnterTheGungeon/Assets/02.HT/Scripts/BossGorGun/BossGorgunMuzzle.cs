using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGorgunMuzzle : MonoBehaviour
{
    // Vector2[] muzzleLeftPosition = new Vector2[] { new Vector2(-60, 120), new Vector2(-120, 45), new Vector2(-90, -20), new Vector2(-20, -40), new Vector2(-10, -1) };
    // Vector2[] muzzleRightPosition = new Vector2[] { new Vector2(60, 120), new Vector2(120, 45), new Vector2(90, -20), new Vector2(30, -40), new Vector2(10, -1) };

    //pattern2
    Vector2 muzzleLeftStartPosition1 = new Vector2(-60, 120);
    Vector2 muzzleLeftEndPosition1 = new Vector2(-20, -40);
    Vector2 muzzleRightStartPosition1 = new Vector2(60, 120);
    Vector2 muzzleRightEndPosition1 = new Vector2(30, -40);
    //pattern2

    //pattern2_1
    Vector2 muzzleLeftStartPosition2 = new Vector2(-130, 80);
    Vector2 muzzleLeftEndPosition2 = new Vector2(-35, -30);
    Vector2 muzzleRightStartPosition2 = new Vector2(125, 42);
    Vector2 muzzleRightEndPosition2 = new Vector2(-30, -35);
    //pattern2_1


    bool isMuzzlePositionSet;

    public bool isLeft;
    public bool isRight;

    public bool isShotBullet;
    Vector2 directionForSummonBullet;
    float xValue;
    float yValue;

    Transform bossGorgun;

    ObjectPool objectPool;
    List<GameObject> enemyBulletPool;
    GameObject enemyBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "muzzleLeft")
        {
            isLeft = true;
        }
        if (this.name == "muzzleRight")
        {
            isRight = true;
        }

        bossGorgun = transform.parent.parent;

        objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        enemyBulletPool = objectPool.enemyBulletPool;
        enemyBulletPrefab = objectPool.enemyBulletPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShotBullet)
        {
            if (isLeft)
            {
                if (bossGorgun.GetComponent<BossGorGun>().isPlayerUpside)
                {
                    transform.localPosition = Vector3.Slerp(transform.localPosition, muzzleLeftEndPosition1, 2 * Time.deltaTime);
                }
                else
                {
                    transform.localPosition = Vector3.Slerp(transform.localPosition, muzzleLeftEndPosition2, 2 * Time.deltaTime);
                }
            }
            if (isRight)
            {
                if (bossGorgun.GetComponent<BossGorGun>().isPlayerUpside)
                {
                    transform.localPosition = Vector3.Slerp(transform.localPosition, muzzleRightEndPosition1, 2 * Time.deltaTime);
                }
                else
                {
                    transform.localPosition = Vector3.Slerp(transform.localPosition, muzzleRightEndPosition2, 2 * Time.deltaTime);

                }
            }

            if (!isDelayEnd)
            {
                StartCoroutine(ShotDelay());
            }
        }
    }
    bool isDelayEnd;
    IEnumerator ShotDelay()
    {
        isDelayEnd = true;
        yield return new WaitForSeconds(0.05f);
        GameObject clone_ = objectPool.GetObject(enemyBulletPool, enemyBulletPrefab, 2);
        clone_.transform.position = transform.position;
        clone_.GetComponent<TestBullet>().bulletType = 0;
        clone_.GetComponent<RectTransform>().localScale = Vector3.one;

        xValue = clone_.transform.position.x - bossGorgun.position.x;
        yValue = clone_.transform.position.y - bossGorgun.position.y;

        directionForSummonBullet = new Vector2(xValue, yValue);
        clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
        isDelayEnd = false;
    }

    public void MoveCheck()
    {
        if (isLeft && isMuzzlePositionSet)
        {
            if (bossGorgun.GetComponent<BossGorGun>().isPlayerUpside)
            {
                transform.localPosition = muzzleLeftStartPosition1;
            }
            else
            {
                transform.localPosition = muzzleLeftStartPosition2;
            }
        }
        if (isRight && isMuzzlePositionSet)
        {
            if (bossGorgun.GetComponent<BossGorGun>().isPlayerUpside)
            {
                transform.localPosition = muzzleRightStartPosition1;
            }
            else
            {
                transform.localPosition = muzzleRightStartPosition2;

            }
        }
        isMuzzlePositionSet = false;
    }
    public void ResetMoveCount()
    {
        isMuzzlePositionSet = true;
    }

}
