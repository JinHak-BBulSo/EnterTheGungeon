using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGorgunBody : MonoBehaviour
{
    float angleForSummonBullet;
    float xValue;
    float yValue;
    Vector2 directionForSummonBullet;

    public int patternCount = 0;

    BossGorgunMuzzle muzzleLeft;
    BossGorgunMuzzle muzzleRight;

    // Start is called before the first frame update
    void Start()
    {
        muzzleLeft = transform.parent.GetChild(2).GetChild(0).GetComponent<BossGorgunMuzzle>();
        muzzleRight = transform.parent.GetChild(2).GetChild(1).GetComponent<BossGorgunMuzzle>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // { add function to animation event 
    public void ShotBulletAttack1Pattern1()
    {
        for (float i = 0; i <= 360; i += 15)
        {
            GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
            clone_.transform.SetParent(GameObject.Find("GameObjs").transform);
            clone_.GetComponent<TestBullet>().bulletType = 0;

            angleForSummonBullet = i * Mathf.PI / 180.0f;
            xValue = Mathf.Cos(angleForSummonBullet);
            yValue = Mathf.Sin(angleForSummonBullet);
            directionForSummonBullet = new Vector2(xValue, yValue);
            clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
        }
    }
    public void ShotBulletAttack2Pattern1()
    {
        for (float i = 5; i <= 360; i += 15)
        {
            GameObject clone_ = Instantiate(Resources.Load<GameObject>("02.HT/Prefabs/TestBullet"), transform.position, transform.rotation);
            clone_.transform.SetParent(GameObject.Find("GameObjs").transform);
            clone_.GetComponent<TestBullet>().bulletType = 0;

            angleForSummonBullet = i * Mathf.PI / 180.0f;
            xValue = Mathf.Cos(angleForSummonBullet);
            yValue = Mathf.Sin(angleForSummonBullet);
            directionForSummonBullet = new Vector2(xValue, yValue);
            clone_.GetComponent<Rigidbody2D>().AddForce(directionForSummonBullet.normalized * 5, ForceMode2D.Impulse);
        }
    }

    public void CountCheck()
    {
        patternCount++;
    }

    public void MoveMuzzle()
    {
        muzzleLeft.MoveCheck();
        muzzleRight.MoveCheck();
    }
    public void ResetMuzzleMoveCount()
    {
        muzzleLeft.ResetMoveCount();
        muzzleRight.ResetMoveCount();
    }

    //type1/2나누기
    public void ShotBulletStart()
    {
        muzzleLeft.isShotBullet = true;
        muzzleRight.isShotBullet = true;
    }
    //type1/2나누기

    public void ShotBulletEnd()
    {
        muzzleLeft.isShotBullet = false;
        muzzleRight.isShotBullet = false;
    }
    // } add function to animation event 

}
