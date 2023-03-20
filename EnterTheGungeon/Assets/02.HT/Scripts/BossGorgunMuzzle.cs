using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGorgunMuzzle : MonoBehaviour
{
    // Vector2[] muzzleLeftPosition = new Vector2[] { new Vector2(-60, 120), new Vector2(-120, 45), new Vector2(-90, -20), new Vector2(-20, -40), new Vector2(-10, -1) };
    // Vector2[] muzzleRightPosition = new Vector2[] { new Vector2(60, 120), new Vector2(120, 45), new Vector2(90, -20), new Vector2(30, -40), new Vector2(10, -1) };

    Vector2 muzzleLeftStartPosition = new Vector2(-60, 120);
    Vector2 muzzleLeftEndPosition = new Vector2(-20, -40);
    Vector2 muzzleRightStartPosition = new Vector2(60, 120);
    Vector2 muzzleRightEndPosition = new Vector2(30, -40);


    bool isMuzzlePositionSet;

    public bool isLeft;
    public bool isRight;

    public bool isShotBullet;
    Vector2 directionForSummonBullet;
    float xValue;
    float yValue;

    Transform bossGorgun;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (isShotBullet)
        {
            if (isLeft)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, muzzleLeftEndPosition, 2 * Time.deltaTime);
            }
            if (isRight)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, muzzleRightEndPosition, 2 * Time.deltaTime);
            }

            GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
            clone_.GetComponent<TestBullet>().bulletType = 0;
            clone_.transform.SetParent(GameObject.Find("GameObjs").transform);

            xValue = clone_.transform.position.x - bossGorgun.position.x;
            yValue = clone_.transform.position.y - bossGorgun.position.y;

            directionForSummonBullet = new Vector2(xValue, yValue);
            clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
        }
    }
    public void MoveCheck()
    {
        if (isLeft && isMuzzlePositionSet)
        {
            transform.localPosition = muzzleLeftStartPosition;
        }
        if (isRight && isMuzzlePositionSet)
        {
            transform.localPosition = muzzleRightStartPosition;
        }
        isMuzzlePositionSet = false;
    }
    public void ResetMoveCount()
    {
        isMuzzlePositionSet = true;
    }

}
